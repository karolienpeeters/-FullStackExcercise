using FullStack.DAL.Models;
using FullStack.DAL.Models.Entities;
using System.Collections.Generic;


namespace FullStack.DAL.Interfaces
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetCustomers();
        Pagination GetCustomersPage(int skip, int take, string filterFirstName, string filterLastName, string filterAccountNumber, decimal filterSumTotalDueHigher, decimal filterSumTotalDueLower);

        Customer GetCustomer(int customerId);
        void UpdateCustomer(Customer customer);
    }
}
