using System;
using System.Collections.Generic;
using System.Text;
using FullStack.DAL.Models.Entities;

namespace FullStack.DAL.Models
{
    public class CustomerFilterPagination
    {
        public CustomerFilterPagination(int pageNumber, int numberItems,string filter)
        {
            Filter = filter;
            PageSize = numberItems;
            CurrentPage = pageNumber;
            CustomerItemList = new List<Customer>();

        }

        public string Filter { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public List<Customer> CustomerItemList { get; set; }
    }
}
