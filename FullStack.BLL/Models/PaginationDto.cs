using FullStack.DAL.Models;
using System.Collections.Generic;

namespace FullStack.BLL.Models
{
    public class PaginationDto
    {
        public PaginationDto(Pagination pagination)
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
