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
    [ServiceFilter(typeof(ApiExceptionFilter))]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Get(int skip, int take, string filterFirstName, string filterLastName,
            string filterAccountNumber, decimal filterSumTotalDueHigher, decimal filterSumTotalDueLower)
        {
            if ((skip == 0) & (take == 0) || take == 0)
                return StatusCode(400,
                    "Something went wrong with getting the list of customers, contact your administrator");

            var customers = _customerService.GetListCustomersPage(skip, take, filterFirstName, filterLastName,
                filterAccountNumber, filterSumTotalDueHigher, filterSumTotalDueLower);

            return Ok(customers);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("updatecustomer")]
        public IActionResult UpdateCustomer([FromBody] CustomerDto customerDto)
        {
            var result = _customerService.UpdateCustomer(customerDto);

            return result > 0 ? Ok(result) : StatusCode(500, "Something went wrong, the customer isn't updated");
        }
    }
}