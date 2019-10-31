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

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet, Authorize]
        public IActionResult Get(int skip, int take, string filterFirstName, string filterLastName, string filterAccountNumber, decimal filterSumTotalDueHigher, decimal filterSumTotalDueLower)
        {
            var customers = _customerService.GetListCustomersPage(skip,take,filterFirstName,filterLastName,filterAccountNumber,filterSumTotalDueHigher,filterSumTotalDueLower);
            //var customers = _customerService.GetListCustomersPage(model.CurrentPage, model.PageSize, model.FilterFirstName, model.FilterLastName, model.FilterAccountNumber, model.FilterSumTotalDueHigher, model.FilterSumTotalDueLower);
            return Ok(customers);
        }

        [HttpPut,Authorize, Route("updatecustomer")]
        public IActionResult UpdateCustomer([FromBody]CustomerDto customerDto)
        {
            _customerService.UpdateCustomer(customerDto);
            return Ok();
        }

        
    }
}