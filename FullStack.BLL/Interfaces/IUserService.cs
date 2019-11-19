using System.Threading.Tasks;
using FullStack.BLL.Models;
using Microsoft.AspNetCore.Identity;

namespace FullStack.BLL.Interfaces
{
    public interface IUserService
    {
        Task<string> HandleLogin(LoginDto userLogin);
        PaginationDto GetUsersWithRoles(int skip, int take);
        Task<IdentityResult> RegisterNewUser(LoginDto userLogin);
        Task<IdentityResult> DeleteUser(string userId);
        Task<IdentityResult> UpdateUser(UserDto userDto);
    }
}