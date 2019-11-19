using FullStack.BLL.Interfaces;
using FullStack.BLL.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FullStack.API.ErrorHandling;

namespace FullStack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    [ServiceFilter(typeof(ApiExceptionFilter))]

    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginDto user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            var token = await _userService.HandleLogin(user);
            if (token == "")
            {
                return Unauthorized("Please enter a correct email and password");
            }

            return Ok(new { Token = token });
        }
    }
}
