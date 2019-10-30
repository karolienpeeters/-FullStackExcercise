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

        public UserRepository(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<IdentityUser> FindByName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<bool> CheckPassword(IdentityUser user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        // GET: ApplicationUserRoles
        public List<User> GetApplicationUsersAndRoles()
        {
            List<User> usersWithRoles = new List<User>();
            var identityUsers = _userManager.Users;

            foreach (var user in identityUsers)
            {
                var roles = _userManager.GetRolesAsync(user).Result.ToList();
                usersWithRoles.Add(new User(user, roles));

            }

            return usersWithRoles;
        }

    }
}
