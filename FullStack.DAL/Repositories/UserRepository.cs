using System;
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
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityUser> _roleManager;

        public UserRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityUser> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        public async Task<IdentityUser> FindByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> CheckPassword(IdentityUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> Create(string email, string password)
        {
            try
            {
                var user = new IdentityUser { UserName = email, Email = email};
                var result =  await _userManager.CreateAsync(user, password);
             
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
 
        }

        // GET: ApplicationUserRoles
        public List<User> GetApplicationUsersAndRoles()
        {
            List<User> usersWithRoles = new List<User>();
            var identityUsers = _userManager.Users;

            foreach (var user in identityUsers)
            {
                var roles = _userManager.GetRolesAsync(user).Result.ToArray();
                usersWithRoles.Add(new User(user, roles));

            }

            return usersWithRoles;
        }


        public async Task<IdentityResult> DeleteUser (string userId)
        {
            var user = _userManager.FindByIdAsync(userId);
            return await _userManager.DeleteAsync(user.Result);

        }

        public void AddRolesToUser(string userId, string[] roles)
        {
            var user = _userManager.FindByIdAsync(userId).Result;

            foreach (var role in roles)
            {
                var roleCheck = _roleManager.RoleExistsAsync(role);

                if (roleCheck.Result)
                {
                    _userManager.AddToRoleAsync(user, role);
                }


            }
        }

    }
}
