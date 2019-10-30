using FullStack.BLL.Interfaces;
using FullStack.BLL.Models;
using FullStack.DAL.Interfaces;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using FullStack.DAL.Models;

namespace FullStack.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository repository )
        {
            _customerRepository = repository;
        }

        public List<CustomerDto> GetListCustomers()
        {
            try
            {
                var customers = _customerRepository.GetCustomers();
                var listCustomersDto = new List<CustomerDto>();

                foreach (var customer in customers)
                {
                    var customerModel = new CustomerDto(customer)
                    {
                        SumTotalDue = customer.SalesOrderHeader.Sum(s => s.TotalDue)
                    };
                    listCustomersDto.Add(customerModel);

                }

                return listCustomersDto.ToList();

            }
            catch (System.Exception)
            {

                throw;
            }


        }

        public CustomerFilterPaginationDto GetListCustomersPage(int skip, int take,string filter)
        {

            


            var paginationFilterModel = skip == 0 ? _customerRepository.GetCustomersPage(skip, take,filter) : _customerRepository.GetCustomersPage(((skip-1)*take), take,filter);



            var filterPaginationDto = new CustomerFilterPaginationDto(paginationFilterModel);

           foreach (var customer in paginationFilterModel.CustomerItemList)
           {
               var customerModel = new CustomerDto(customer)
               {
                   SumTotalDue = customer.SalesOrderHeader.Sum(s => s.TotalDue)
               };
               filterPaginationDto.CustomerItemList.Add(customerModel);

           }

           return filterPaginationDto;

        }
    }
}
