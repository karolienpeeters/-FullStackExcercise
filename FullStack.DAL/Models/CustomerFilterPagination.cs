using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using FullStack.DAL.Models.Entities;

namespace FullStack.DAL.Models
{
    public class CustomerFilterPagination
    {
        public CustomerFilterPagination()
        {
            CustomerItemList = new List<Customer>();
        }

        public int TotalItems { get; set; }
     
        public List<Customer> CustomerItemList { get; set; }
    }
}
