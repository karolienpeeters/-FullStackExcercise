using FullStack.DAL.Models.Entities;

namespace FullStack.BLL.Models
{
    public class CustomerDto
    {
        public CustomerDto()
        {
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