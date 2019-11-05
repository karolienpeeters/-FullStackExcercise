using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FullStack.BLL.Interfaces;
using FullStack.BLL.Models;
using FullStack.DAL.Interfaces;
using FullStack.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FullStack.BLL.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> HandleLogin(LoginDto loginDto)
        {
            
            var theUser = await _userRepository.FindByName(loginDto.Email);
            if (theUser != null && await _userRepository.CheckPassword(theUser,loginDto.PassWord))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("fullstack_951357456"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:44318",
                    audience: "*",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return tokenString;


            }

            return "";

        }

        public List<UserDto> GetUsersWithRoles()
        {
            var usersWithRoles = _userRepository.GetApplicationUsersAndRoles();

            var listUsers = usersWithRoles.Select(userItem => new UserDto(userItem)).ToList();

            return listUsers;
        }

        public async Task<IdentityResult> RegisterNewUser(LoginDto loginDto)
        {
            try
            {
                var result = await _userRepository.Create(loginDto.Email, loginDto.PassWord);
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
                var result = await _userRepository.DeleteUser(userId);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }

        public async Task<IdentityResult> UpdateUser(UserDto userDto)
        {
            var user = _userRepository.FindById(userDto.UserId).Result;
            user.Email = userDto.Email;
            user.UserName = userDto.Email;

            try
            {
                var result = await _userRepository.UpdateUser(user, userDto.RolesList);
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
