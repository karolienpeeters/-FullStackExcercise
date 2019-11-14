using FullStack.BLL.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullStack.BLL.Interfaces
{
    public interface IUserService
    {
        Task<string> HandleLogin(UserDto userLogin);
        PaginationDto GetUsersWithRoles(int skip, int take);
        Task<IdentityResult> RegisterNewUser(UserDto userLogin);
        Task<IdentityResult> DeleteUser(string userId);
        Task<IdentityResult> UpdateUser(UserDto userDto);

        //Task<IdentityResult> Update(UserDto userDto);

    }
}
