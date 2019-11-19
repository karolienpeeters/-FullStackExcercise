using System;
using System.Linq;
using FluentValidation;
using FullStack.BLL.Common;
using FullStack.BLL.Interfaces;
using FullStack.BLL.Models;
using FullStack.DAL.Interfaces;
using FullStack.DAL.Models.Entities;
using FullStack.DAL.Validators;
using Microsoft.AspNetCore.Mvc.Internal;
using Westwind.Utilities;

namespace FullStack.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IValidator<Customer> _customerValidator;

        public CustomerService(ICustomerRepository repository, IValidator<Customer> customerValidator)
        {
            _customerRepository = repository;
            _customerValidator = customerValidator;
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
                Console.WriteLine(e);
                throw new ApiException(e);
                
            }
        }

        public int UpdateCustomer(CustomerDto customerDto)
        {
            try
            {
                var customer = _customerRepository.GetCustomer(customerDto.Id);

                if (customer == null) throw new ApiException("The customer you want to change does not exist");

                customer.Person.FirstName =customerDto.FirstName;
                customer.Person.LastName = customerDto.LastName;

               var result =  _customerValidator.Validate(customer);

               if (!result.IsValid)
               {
                   throw new ApiException(string.Join(" ~ ",result.Errors.Select(failure => failure.ErrorMessage)));
               }

                _customerRepository.UpdateCustomer(customer);

                return _customerRepository.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new ApiException(e);
            }
        }
    }
}