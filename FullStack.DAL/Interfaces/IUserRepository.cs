using System;
using System.Collections.Generic;
using System.Text;
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
        List<User> GetApplicationUsersAndRoles();

        Task<IdentityResult> Create(string userName, string password);

        Task<IdentityResult> DeleteUser(string userId);

        Task<IdentityResult> UpdateUser(IdentityUser iUser, List<string> roles);
        Task<IList<string>> GetRolesUser(IdentityUser iUser);



    }
}
