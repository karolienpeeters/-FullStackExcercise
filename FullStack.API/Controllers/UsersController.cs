using System.Linq;
using System.Threading.Tasks;
using FullStack.API.ErrorHandling;
using FullStack.BLL.Interfaces;
using FullStack.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FullStack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Authorize(Roles = "Admin")]
    [ServiceFilter(typeof(ApiExceptionFilter))]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get(int skip, int take)
        {
            if ((skip == 0) & (take == 0) || take == 0)
                return StatusCode(400,
                    "Something went wrong with getting the list of user, contact your administrator");

            var users = _userService.GetUsersWithRoles(skip, take);

            return Ok(users);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] LoginDto userDto)
        {
            var result = await _userService.RegisterNewUser(userDto);

            return !result.Succeeded ? StatusCode(400, result.Errors.First()) : Ok(result);
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userService.DeleteUser(id);

            return !result.Succeeded ? StatusCode(400, result.Errors.First()) : Ok(result);
        }

        [HttpPut("updateuser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto)
        {
            var result = await _userService.UpdateUser(userDto);

            return !result.Succeeded ? StatusCode(400, result.Errors.First()) : Ok(result);
        }
    }
}