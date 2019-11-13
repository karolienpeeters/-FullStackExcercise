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

        public async Task<IdentityResult> CreateUser(string userName, string password)
        { 
            return await _userManager.CreateAsync(new IdentityUser { UserName = userName, Email = userName }, password);
        }

        // GET: ApplicationUserRoles
        public Pagination GetApplicationUsersAndRoles(int skip, int take)
        {

            var userPage = new Pagination();
           
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

        public async Task<IdentityResult> DeleteUser (IdentityUser iUser)
        {
            return await _userManager.DeleteAsync(iUser);
        }

        public async Task<IdentityResult> UpdateUser(IdentityUser iUser)
        {
            return await _userManager.UpdateAsync(iUser);
        }

        public async Task<IdentityResult> AddRoles (IdentityUser iUser,IList<string> userRoles, List<string> roles)
        {
            return await _userManager.AddToRolesAsync(iUser, roles.Except(userRoles).ToList());
        }

        public async Task<IdentityResult> RemoveRoles(IdentityUser iUser, IList<string> userRoles, List<string> roles)
        {
            return  await _userManager.RemoveFromRolesAsync(iUser, userRoles.Except(roles).ToList());
        }

        //public async Task<IdentityResult> Update(IdentityUser iUser, IList<string> userRoles, List<string> roles)
        //{
        //    if (roles.Any())
        //    {
                
        //    }
        //    await _userManager.AddToRolesAsync(iUser, roles.Except(userRoles).ToList());

        //    await _userManager.RemoveFromRolesAsync(iUser, userRoles.Except(roles).ToList());

        //    return await _userManager.UpdateAsync(iUser);

            
        //}

       
    }
}
