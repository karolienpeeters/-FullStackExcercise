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
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                if (result.Succeeded)
                { 

                    await _signInManager.SignInAsync(user, isPersistent: false);
                   
                }

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
                var roles = GetRolesUser(user).Result.ToList();
                usersWithRoles.Add(new User(user, roles));

            }

            return usersWithRoles;
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
                   await _userManager.AddToRoleAsync(iUser, role);
                }


            }

            return await _userManager.UpdateAsync(iUser);
        }

    }
}
