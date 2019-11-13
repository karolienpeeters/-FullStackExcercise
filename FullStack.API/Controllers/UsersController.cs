using FullStack.BLL.Interfaces;
using FullStack.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
            return Ok(result);

        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody]UserDto userDto)
        {
            var result = await _userService.RegisterNewUser(userDto);
            return Ok(result);

        }

        [Route("delete")]
        public async Task<IActionResult> Delete(string userId)
        {
            var result = await _userService.DeleteUser(userId);
            return Ok(result);
        }

        [HttpPut("updateuser/{uid}")]
        public async Task<IActionResult> UpdateUser([FromBody]UserDto userDto, string uid)
        {
            var result = await _userService.UpdateUser(userDto);
            //var result = await _userService.Update(userDto);

            return Ok(result);
        }



    }
}