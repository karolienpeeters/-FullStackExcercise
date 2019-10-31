using FullStack.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullStack.BLL.Interfaces
{
    public interface ICustomerService
    {
        List<CustomerDto> GetListCustomers();
        CustomerFilterPaginationDto GetListCustomersPage(int skip, int take, string filterFirstName, string filterLastName, string filterAccountNumber, decimal filterSumTotalDueHigher, decimal filterSumTotalDueLower);
        void UpdateCustomer(CustomerDto customerDto);
    }
}
