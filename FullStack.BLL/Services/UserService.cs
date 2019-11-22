using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FullStack.BLL.Common;
using FullStack.BLL.Interfaces;
using FullStack.BLL.Models;
using FullStack.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace FullStack.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<string> HandleLogin(LoginDto userLogin)
        {
            try
            {
                var theUser = await _userRepository.FindByName(userLogin.Email);
                if (theUser != null && await _userRepository.CheckPassword(theUser, userLogin.PassWord))
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("fullstack_951357456"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var roles = await _userRepository.GetRolesUser(theUser);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userLogin.Email)
                    };
                    foreach (var item in roles) claims.Add(new Claim(ClaimTypes.Role, item));
                    var tokeOptions = new JwtSecurityToken(
                        "https://localhost:44318",
                        "*",
                        claims,
                        expires: DateTime.Now.AddMinutes(120),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    return tokenString;
                }

                return "";
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.Message);
                throw new ApiException(e);
            }
        }

        public PaginationDto GetUsersWithRoles(int skip, int take)
        {
            try
            {
                var usersPagination = _userRepository.GetApplicationUsersAndRoles(skip, take);
                var usersPaginationDto = new PaginationDto(usersPagination)
                {
                    UserList = usersPagination.UserList.Select(userItem => new UserDto(userItem)).ToList()
                };

                return usersPaginationDto;
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.Message);

                throw new ApiException(e);

            }
        }

        public async Task<IdentityResult> RegisterNewUser(LoginDto userDto)
        {
            try
            {
                var result = await _userRepository.CreateUser(userDto.Email, userDto.PassWord);

                return result;
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.Message);

                throw new ApiException("Something went wrong with creating a new user, contact your administrator");
            }
        }

        public async Task<IdentityResult> DeleteUser(string userId)
        {
            try
            {
                var user = await GetUserById(userId);

                return await _userRepository.DeleteUser(user);
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.Message);

                throw;
            }
        }

        public async Task<IdentityResult> UpdateUser(UserDto userDto)
        {
            try
            {
                var user = await GetUserById(userDto.Id);

                user.Email = userDto.Email;
                user.UserName = userDto.Email;

                var userRoles = await _userRepository.GetRolesUser(user);

                var result = await _userRepository.RemoveRoles(user, userRoles, userDto.RolesList);

                if (!result.Succeeded)
                    throw new ApiException(
                        "Something went wrong with removing roles, please contact your web administrator");

                result = await _userRepository.AddRoles(user, userRoles, userDto.RolesList);

                if (!result.Succeeded)
                    throw new ApiException(
                        "Something went wrong with adding roles, please contact your web administrator");

                result = await _userRepository.UpdateUser(user);

                if (!result.Succeeded)
                    throw new ApiException(
                        "Something went wrong with updating the user, please contact your web administrator");
              
                return result;
            }
            catch (ApiException e)
            {
                _logger.LogDebug(e.Message);

                throw new ApiException(e);

            }
        }

        private async Task<IdentityUser> GetUserById(string userId)
        {
            try
            {
                var user = await _userRepository.FindById(userId);

                if (user == null) throw new ApiException("The user does not exist");

                return user;
            }
            catch (Exception e)
            {
                _logger.LogDebug(e.Message);

                throw new ApiException(e);
            }
        }
    }
}