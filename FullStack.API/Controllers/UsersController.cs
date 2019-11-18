using System.Linq;
using FullStack.BLL.Interfaces;
using FullStack.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FullStack.BLL.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;

namespace FullStack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Authorize(Roles = "Admin")]
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
            var users = _userService.GetUsersWithRoles(skip, take);
            return Ok(users);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]UserDto userDto)
        {
            var result = await _userService.RegisterNewUser(userDto);
            return !result.Succeeded ? StatusCode(500, result.Errors.First()) : Ok(result);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody]UserDto userDto)
        {
            var result = await _userService.RegisterNewUser(userDto);

            return !result.Succeeded ? StatusCode(500, result.Errors.First()) : Ok(result);
        }

        [Route("delete")]
        public async Task<IActionResult> Delete(string userId)
        {
            var result = await _userService.DeleteUser("5");

            return !result.Succeeded ? StatusCode(500, result.Errors.First()) : Ok(result);
        }

        [HttpPut("updateuser")]
        public async Task<IActionResult> UpdateUser([FromBody]UserDto userDto, string uid)
        {
            var result = await _userService.UpdateUser(userDto);

            return !result.Succeeded ? StatusCode(500,result.Errors.First()) : Ok(result);
        }



    }
}