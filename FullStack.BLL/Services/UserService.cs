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

        public PaginationDto GetUsersWithRoles(int skip, int take)
        {
            var usersPagination = _userRepository.GetApplicationUsersAndRoles(skip,take);
            var usersPaginationDto = new PaginationDto(usersPagination)
            {
                UserList = usersPagination.UserList.Select(userItem => new UserDto(userItem)).ToList()
            };

            return usersPaginationDto;
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
               return  await _userRepository.DeleteUser(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<List<IdentityResult>> UpdateUser(UserDto userDto)
        {
            var listResult =new List<IdentityResult>();
            try
            {
                var user = _userRepository.FindById(userDto.UserId).Result;
                user.Email = userDto.Email;
                user.UserName = userDto.Email;
                var userRoles = _userRepository.GetRolesUser(user).Result;
                listResult.Add(await _userRepository.AddRoles(user, userRoles, userDto.RolesList));
                listResult.Add(await _userRepository.RemoveRoles(user, userRoles, userDto.RolesList));
                listResult.Add(await _userRepository.UpdateUser(user));

               return listResult;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        //public async Task<IdentityResult> Update(UserDto userDto)
        //{
        //    try
        //    {
        //        var user = _userRepository.FindById(userDto.UserId).Result;
        //        user.Email = userDto.Email;
        //        user.UserName = userDto.Email;
        //        var userRoles = _userRepository.GetRolesUser(user).Result;
               
        //        return await _userRepository.Update(user, userRoles, userDto.RolesList);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }


        //}

    }
}
