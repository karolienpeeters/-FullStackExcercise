using System.Collections.Generic;
using System.Threading.Tasks;
using FullStack.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace FullStack.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<IdentityUser> FindByName(string userName);
        Task<IdentityUser> FindById(string userId);
        Task<bool> CheckPassword(IdentityUser user, string password);
        Pagination GetApplicationUsersAndRoles(int skip, int take);
        Task<IdentityResult> CreateUser(string userName, string password);
        Task<IdentityResult> DeleteUser(IdentityUser iUser);
        Task<IdentityResult> UpdateUser(IdentityUser iUser);
        Task<IList<string>> GetRolesUser(IdentityUser iUser);
        Task<IdentityResult> AddRoles(IdentityUser iUser, IList<string> userRoles, List<string> roles);
        Task<IdentityResult> RemoveRoles(IdentityUser iUser, IList<string> userRoles, List<string> roles);
    }
}