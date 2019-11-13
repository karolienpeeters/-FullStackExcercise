using System;
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
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet, Authorize]
        public IActionResult Get(int skip, int take, string filterFirstName, string filterLastName, string filterAccountNumber, string filterSumTotalDueHigher, decimal filterSumTotalDueLower)
        {
            var customers = _customerService.GetListCustomersPage(skip,take,filterFirstName,filterLastName,filterAccountNumber,decimal.Parse(filterSumTotalDueHigher), filterSumTotalDueLower);
            return Ok(customers);
        }

        [HttpPut, Authorize(Roles = "Admin"), Route("updatecustomer")]
        public IActionResult UpdateCustomer([FromBody]CustomerDto customerDto)
        {
            _customerService.UpdateCustomer(customerDto);
            return Ok();
        }

        
    }
}