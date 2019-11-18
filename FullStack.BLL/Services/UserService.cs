using FullStack.BLL.Interfaces;
using FullStack.BLL.Models;
using FullStack.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FullStack.BLL.Common;


namespace FullStack.BLL.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> HandleLogin(UserDto userLogin)
        {
            
            var theUser = await _userRepository.FindByName(userLogin.Email);
            if (theUser != null && await _userRepository.CheckPassword(theUser, userLogin.PassWord))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("fullstack_951357456"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var roles = await _userRepository.GetRolesUser(theUser);
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, userLogin.Email));
                foreach (string item in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:44318",
                    audience: "*",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(50),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return tokenString;


            }

            return "";

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
                Console.WriteLine(e);
                throw;
            }
            
        }

        public async Task<IdentityResult> RegisterNewUser(UserDto userDto)
        {
            try
            {
                var result = await _userRepository.CreateUser(userDto.Email, userDto.PassWord);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IdentityResult> DeleteUser(string userId)
        {
            try
            {
               var user = _userRepository.FindById(userId).Result;
               if (user == null)
               {
                   throw new ApiException("The user you want to delete does not exist");
               }
               return  await _userRepository.DeleteUser(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<IdentityResult> UpdateUser(UserDto userDto)
        {
            
            try
            {
                var user = await _userRepository.FindById(userDto.Id);

                if (user == null)
                {
                    throw new ApiException("The user you want to change does not exist");
                }

                //userDTO rolelist opvangen via validatie model
                user.Email = userDto.Email;
                user.UserName = userDto.Email;

                var userRoles = await _userRepository.GetRolesUser(user);

                var result = await _userRepository.RemoveRoles(user, userRoles, userDto.RolesList);

                if (!result.Succeeded)
                {
                    throw new ApiException("Something went wrong with removing roles, please contact your web administrator");
                }

                result = await _userRepository.AddRoles(user, userRoles, userDto.RolesList);

                if (!result.Succeeded)
                {
                    throw new ApiException("Something went wrong with adding roles, please contact your web administrator");
                }

                result = await _userRepository.UpdateUser(user);

                if (!result.Succeeded)
                {
                    throw new ApiException("Something went wrong with updating the user, please contact your web administrator");
                }

                return result;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }


    }
}
