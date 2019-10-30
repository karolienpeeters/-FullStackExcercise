using FullStack.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using FullStack.DAL.Models.Entities;


namespace FullStack.DAL.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        CustomerFilterPagination GetCustomersPage(int skip, int take, string filterFirstName, string filterLastName, string filterAccountNumber, decimal filterSumTotalDueHigher, decimal filterSumTotalDueLower);


    }
}
