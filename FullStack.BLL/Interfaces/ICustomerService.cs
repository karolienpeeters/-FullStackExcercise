using FullStack.BLL.Models;

namespace FullStack.BLL.Interfaces
{
    public interface ICustomerService
    {
        PaginationDto GetListCustomersPage(int skip, int take, string filterFirstName, string filterLastName,
            string filterAccountNumber, decimal filterSumTotalDueHigher, decimal filterSumTotalDueLower);

        int UpdateCustomer(CustomerDto customerDto);
    }
}