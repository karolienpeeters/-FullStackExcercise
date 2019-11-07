using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using FullStack.DAL.Models;

namespace FullStack.BLL.Models
{
    public class CustomerFilterPaginationDto
    {
        public CustomerFilterPaginationDto(CustomerFilterPagination customerFilterPagination)
        {
            TotalItems = customerFilterPagination.TotalItems;
            CustomerItemList = new List<CustomerDto>();
        }

        public int TotalItems { get; set; }
      
        public List<CustomerDto> CustomerItemList { get; set; }
    }
}
