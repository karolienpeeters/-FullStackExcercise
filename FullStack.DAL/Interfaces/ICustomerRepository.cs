using FullStack.DAL.Models;
using FullStack.DAL.Models.Entities;

namespace FullStack.DAL.Interfaces
{
    public interface ICustomerRepository
    {
        Pagination GetCustomersPage(int skip, int take, string filterFirstName, string filterLastName,
            string filterAccountNumber, decimal filterSumTotalDueHigher, decimal filterSumTotalDueLower);

        Customer GetCustomer(int customerId);
        void UpdateCustomer(Customer customer);
        int SaveChanges();
    }
}