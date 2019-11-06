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
            PageSize = customerFilterPagination.PageSize;
            CurrentPage = customerFilterPagination.CurrentPage;
            TotalItems = customerFilterPagination.TotalItems;
            FilterFirstName = customerFilterPagination.FilterFirstName;
            FilterLastName = customerFilterPagination.FilterLastName;
            FilterAccountNumber = customerFilterPagination.FilterAccountNumber;
            FilterSumTotalDueHigher = customerFilterPagination.FilterSumTotalDueHigher;
            FilterSumTotalDueLower = customerFilterPagination.FilterSumTotalDueLower;
            CustomerItemList = new List<CustomerDto>();

        }

        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FilterFirstName { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FilterLastName { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string FilterAccountNumber { get; set; }
        public decimal FilterSumTotalDueHigher { get; set; }
        public decimal FilterSumTotalDueLower { get; set; }

        public List<CustomerDto> CustomerItemList { get; set; }
    }
}
