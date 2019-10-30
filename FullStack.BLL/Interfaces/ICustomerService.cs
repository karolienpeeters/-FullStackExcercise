using FullStack.BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FullStack.BLL.Interfaces
{
    public interface ICustomerService
    {
        List<CustomerDto> GetListCustomers();
        CustomerFilterPaginationDto GetListCustomersPage(int skip, int take,string filter);

    }
}
