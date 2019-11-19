using System.Linq;
using FullStack.DAL.Interfaces;
using FullStack.DAL.Models;
using FullStack.DAL.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FullStack.DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }


        public Pagination GetCustomersPage(int skip, int take, string filterFirstName, string filterLastName,
            string filterAccountNumber, decimal filterSumTotalDueHigher, decimal filterSumTotalDueLower)
        {
            var paginationFilterModel = new Pagination();
            var query = _context.Customers
                .Include("Person")
                .Include("SalesOrderHeader")
                .Where(c => c.Person != null);


            if (!string.IsNullOrEmpty(filterFirstName) && filterFirstName != "null")
                query = query.Where(c => c.Person.FirstName.ToLower().Contains(filterFirstName.ToLower()));

            if (!string.IsNullOrEmpty(filterLastName) && filterLastName != "null")
                query = query.Where(c => c.Person.LastName.ToLower().Contains(filterLastName.ToLower()));

            if (!string.IsNullOrEmpty(filterAccountNumber) && filterAccountNumber != "null")
                query = query.Where(c => c.AccountNumber.ToLower().Contains(filterAccountNumber.ToLower()));

            if (filterSumTotalDueHigher != 0)
                query = query.Where(c => c.SalesOrderHeader.Sum(s => s.TotalDue) > filterSumTotalDueHigher);

            if (filterSumTotalDueLower != 0)
                query = query.Where(c => c.SalesOrderHeader.Sum(s => s.TotalDue) < filterSumTotalDueLower);


            paginationFilterModel.TotalItems = query.Count();

            paginationFilterModel.CustomerItemList = query.Skip(skip).Take(take).ToList();

            return paginationFilterModel;
        }

        public void UpdateCustomer(Customer customer)
        {
          var result =  _context.Customers.Update(customer);
        }

        public Customer GetCustomer(int customerId)
        {
            return _context.Customers.Include(c => c.Person).AsNoTracking()
                .FirstOrDefault(d => d.CustomerId == customerId);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}