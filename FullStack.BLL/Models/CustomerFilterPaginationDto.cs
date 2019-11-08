using FullStack.DAL.Models;
using System.Collections.Generic;

namespace FullStack.BLL.Models
{
    public class CustomerFilterPaginationDto
    {
        public CustomerFilterPaginationDto(Pagination pagination)
        {
            TotalItems = pagination.TotalItems;
            CustomerList = new List<CustomerDto>();
            UserList = new List<UserDto>();
        }

        public int TotalItems { get; set; }
      
        public List<CustomerDto> CustomerList { get; set; }
        public List<UserDto> UserList { get; set; }
    }
}
