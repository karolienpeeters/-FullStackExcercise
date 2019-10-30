using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullStack.BLL.Interfaces;
using FullStack.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullStack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        //private readonly IUserService _userService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
           
        }
        
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var customers = _customerService.GetListCustomers();
        //    return Ok(customers);
        //}

        [HttpGet,Authorize]
        public IActionResult Get(int skip=0, int take=0,string filter="")
        {
            var customers = _customerService.GetListCustomersPage(skip,take,filter);
            return Ok(customers);
        }

        //[HttpGet("{skip}/{take}/{filter}")]
        //public IActionResult Get(int skip, int take, string filter)
        //{
        //    var customers = _customerService.GetListCustomersPage(skip, take, filter);
        //    return Ok(customers);
        //}
    }
}