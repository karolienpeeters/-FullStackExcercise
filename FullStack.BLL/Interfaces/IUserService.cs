using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FullStack.BLL.Models;
using Microsoft.AspNetCore.Identity;

namespace FullStack.BLL.Interfaces
{
    public interface IUserService
    {
        Task<string> HandleLogin(UserDto userLogin);
        List<UserDto> GetUsersWithRoles();
        Task<IdentityResult> RegisterNewUser(UserDto userLogin);
        Task<IdentityResult> DeleteUser(string userId);
        Task<IdentityResult> UpdateUser(UserDto userDto);

    }
}
