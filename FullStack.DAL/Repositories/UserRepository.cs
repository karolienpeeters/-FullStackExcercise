using FullStack.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

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
    }
}
