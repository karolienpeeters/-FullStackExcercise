using FullStack.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace FullStack.DAL.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        CustomerFilterPagination GetCustomersPage(int skip, int take,string filter);

        int GetTotalNumberCustomers();
    }
}
