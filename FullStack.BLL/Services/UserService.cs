﻿using System;
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

        public async Task<IdentityResult> RegisterNewUser(UserDto userLogin)
        {
            try
            {
                var result = await _userRepository.Create(userLogin.Email, userLogin.PassWord);
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
            try
            {
                var user = _userRepository.FindById(userDto.UserId).Result;
                user.Email = userDto.Email;
                user.UserName = userDto.Email;
                return await _userRepository.UpdateUser(user, userDto.RolesList); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }

    }
}
