using FullStack.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullStack.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityUser> FindByName(string userName);
        Task<IdentityUser> FindById(string userId);
        Task<bool> CheckPassword(IdentityUser user, string password);
        Pagination GetApplicationUsersAndRoles(int skip, int take);

        Task<IdentityResult> Create(string userName, string password);

        Task<IdentityResult> DeleteUser(string userId);

        Task<IdentityResult> UpdateUser(IdentityUser iUser, List<string> roles);
        Task<IList<string>> GetRolesUser(IdentityUser iUser);



    }
}
