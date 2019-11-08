using FullStack.DAL.Interfaces;
using FullStack.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using FullStack.DAL.Models.Entities;

namespace FullStack.BLL.Models
{

    public class CustomerDto
    {
        public CustomerDto() { }

        public CustomerDto(int id, string firstName, string lastName, string accountNumber)
        {
            Id = id;
            AccountNumber = accountNumber;
            FirstName = firstName;
            LastName = lastName;
            SumTotalDue = 0;
           
        }

        public CustomerDto(Customer customer)
        {
            Id = customer.CustomerId;
            AccountNumber = customer.AccountNumber;
            FirstName = customer.Person.FirstName;
            LastName = customer.Person.LastName;
            SumTotalDue = 0;
         
        }

        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal SumTotalDue { get; set; }
       



    }
}
