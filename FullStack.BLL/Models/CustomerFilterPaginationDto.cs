using System;
using System.Collections.Generic;
using System.Text;
using FullStack.DAL.Models;

namespace FullStack.BLL.Models
{
    public class CustomerFilterPaginationDto
    {
        public CustomerFilterPaginationDto(CustomerFilterPagination customerFilterPagination)
        {
            PageSize = customerFilterPagination.PageSize;
            CurrentPage = customerFilterPagination.CurrentPage;
            TotalItems = customerFilterPagination.TotalItems;
            CustomerItemList = new List<CustomerDto>();

        }

        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public string FilterFirstName { get; set; }
        public string FilterLastName { get; set; }
        public string FilterAccountNumber { get; set; }
        public decimal FilterSumTotalDueHigher { get; set; }
        public decimal FilterSumTotalDueLower { get; set; }

        public List<CustomerDto> CustomerItemList { get; set; }
    }
}
