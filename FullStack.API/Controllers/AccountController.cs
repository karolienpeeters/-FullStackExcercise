using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FullStack.BLL.Interfaces;
using FullStack.BLL.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FullStack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class AccountController : ControllerBase
    {
        readonly UserManager<IdentityUser> _userManager;

        private readonly IUserService _userService;
       

        public AccountController(UserManager<IdentityUser> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }




        [HttpPost, Route("login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginDto login)
        {
            if (login == null)
            {
                return BadRequest("Invalid client request");
            }
            var theUser = await _userManager.FindByNameAsync(login.UserName);
            if (theUser != null && await _userManager.CheckPasswordAsync(theUser, login.PassWord))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("fullstack_951357456"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:44318",
                    audience: "*",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
