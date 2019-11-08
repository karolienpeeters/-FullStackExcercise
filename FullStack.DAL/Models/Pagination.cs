using FullStack.DAL.Models.Entities;
using System.Collections.Generic;

namespace FullStack.DAL.Models
{
    public class Pagination
    {
        public Pagination()
        {
            CustomerItemList = new List<Customer>();
            UserList = new List<User>();
        }

        public int TotalItems { get; set; }
     
        public List<Customer> CustomerItemList { get; set; }

        public List<User> UserList { get; set; }
    }
}
