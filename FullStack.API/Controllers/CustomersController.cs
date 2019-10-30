using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FullStack.BLL.Interfaces;
using FullStack.BLL.Models;
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

        [HttpGet]
        public IActionResult Get(int skip, int take,string filterFirstName,string filterLastName,string filterAccountNumber,decimal filterSumTotalDueHigher,decimal filterSumTotalDueLower)
        {
            var customers = _customerService.GetListCustomersPage(skip,take,filterFirstName,filterLastName,filterAccountNumber,filterSumTotalDueHigher,filterSumTotalDueLower);
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