using FullStack.BLL.Common;
using FullStack.BLL.Interfaces;
using FullStack.BLL.Models;
using FullStack.DAL.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace FullStack.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<CustomerService> _logger;



        public CustomerService(ICustomerRepository repository, ILogger<CustomerService> logger)
        {
            _customerRepository = repository;
            _logger = logger;
        }


        public PaginationDto GetListCustomersPage(int skip, int take, string filterFirstName, string filterLastName,
            string filterAccountNumber, decimal filterSumTotalDueHigher, decimal filterSumTotalDueLower)
        {
            try
            {
                var paginationModel = _customerRepository.GetCustomersPage((skip - 1) * take, take, filterFirstName,
                    filterLastName, filterAccountNumber, filterSumTotalDueHigher, filterSumTotalDueLower);

                var paginationDto = new PaginationDto(paginationModel);

                foreach (var customer in paginationModel.CustomerItemList)
                {
                    var customerModel = new CustomerDto(customer)
                    {
                        SumTotalDue = customer.SalesOrderHeader.Sum(s => s.TotalDue)
                    };
                    paginationDto.CustomerList.Add(customerModel);
                }

                return paginationDto;
            }
            catch (Exception e)
            {
                _logger.LogDebug("get list customers",e);

                throw new ApiException(e);
                
            }
        }

        public int UpdateCustomer(CustomerDto customerDto)
        {
            try
            {
                var customer = _customerRepository.GetCustomer(customerDto.Id);

                if (customer == null) throw new NullReferenceException("The customer you want to change does not exist");

                customer.Person.FirstName =customerDto.FirstName;
                customer.Person.LastName = customerDto.LastName;

                _customerRepository.UpdateCustomer(customer);

                return _customerRepository.SaveChanges();
            }
            catch (Exception e)
            {
                _logger.LogDebug("update customer",e);

                throw new ApiException(e);
            }
        }
    }
}