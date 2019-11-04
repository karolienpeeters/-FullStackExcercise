using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullStack.BLL.Interfaces;
using FullStack.BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullStack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = _userService.GetUsersWithRoles();
            return Ok(users);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]LoginDto loginDto)
        {
          var result =  await _userService.RegisterNewUser(loginDto);
            return Ok(result);

        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody]LoginDto loginDto)
        {
            var result = await _userService.RegisterNewUser(loginDto);
            return Ok(result);

        }

        [Route("delete")]
        public async Task<IActionResult> Delete(string userId)
        {
            var result = await _userService.DeleteUser(userId);
            return Ok(result);
        }

        [HttpPut("updateuser/{uid}")]
        public IActionResult UpdateUser([FromBody]UserDto userDto, string uid)
        {

            return Ok();
        }



    }
}