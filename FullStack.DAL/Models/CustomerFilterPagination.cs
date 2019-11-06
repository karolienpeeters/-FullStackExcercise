using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using FullStack.DAL.Models.Entities;

namespace FullStack.DAL.Models
{
    public class CustomerFilterPagination
    {
        public CustomerFilterPagination(int pageNumber, int numberItems)
        {
            FilterFirstName = "";
            FilterLastName = "";
            FilterAccountNumber = "";
            FilterSumTotalDueHigher = 0;
            FilterSumTotalDueLower = 0;
            PageSize = numberItems;
            CurrentPage = pageNumber;
            CustomerItemList = new List<Customer>();

        }

        public string FilterFirstName { get; set; }
        public string FilterLastName { get; set; }
        public string FilterAccountNumber { get; set; }
        public decimal FilterSumTotalDueHigher{ get; set; }
        public decimal FilterSumTotalDueLower { get; set; }
      
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public List<Customer> CustomerItemList { get; set; }
    }
}
