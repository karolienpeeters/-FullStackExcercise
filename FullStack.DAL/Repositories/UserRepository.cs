﻿using System;
using System.Collections.Generic;
using System.Linq;
using FullStack.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using FullStack.DAL.Models;

namespace FullStack.DAL.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        public async Task<IdentityUser> FindByName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<IdentityUser> FindById (string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<bool> CheckPassword(IdentityUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> Create(string userName, string password)
        {
            try
            {
                var user = new IdentityUser { UserName = userName, Email = userName};
                var result =  await _userManager.CreateAsync(user, password);
                await AddRole(user, "User");

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
             
        }

        // GET: ApplicationUserRoles
        public Pagination GetApplicationUsersAndRoles(int skip, int take)
        {

            var userPage = new Pagination();
          
            //var identityUsers = _userManager.Users.Skip((skip ==0 ) ? skip : (skip-1)*take).Take(take);
            var identityUsers = _userManager.Users.Skip((skip-1)*take).Take(take);
            userPage.TotalItems = _userManager.Users.Count();

            foreach (var user in identityUsers)
            {
                var roles = GetRolesUser(user).Result.ToList();
                userPage.UserList.Add(new User(user, roles));

            }
            

            return userPage;
        }

        public async Task<IList<string>> GetRolesUser(IdentityUser iUser)
        {
            return await _userManager.GetRolesAsync(iUser);
        }

        public async Task<IdentityResult> DeleteUser (string userId)
        {
            var user = _userManager.FindByIdAsync(userId);
            return await _userManager.DeleteAsync(user.Result);

        }

        public async Task<IdentityResult> UpdateUser(IdentityUser iUser, List<string> roles)
        {
           

            foreach (var role in roles)
            {
                var roleCheck = _roleManager.RoleExistsAsync(role);

                if (roleCheck.Result)
                {
                   await AddRole(iUser, role);
                }


            }

            return await _userManager.UpdateAsync(iUser);
        }

        public async Task<IdentityResult> AddRole(IdentityUser iUser, string role)
        {
            return await _userManager.AddToRoleAsync(iUser, role);
        }

    }
}
