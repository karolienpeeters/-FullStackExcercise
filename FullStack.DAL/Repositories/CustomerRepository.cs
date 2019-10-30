using FullStack.DAL.Interfaces;
using FullStack.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using FullStack.DAL.Models.Entities;

namespace FullStack.DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.Include("Person").Include("SalesOrderHeader").Where(c => c.Person != null);
        }

        public CustomerFilterPagination GetCustomersPage(int skip, int take, string filterFirstName, string filterLastName, 
            string filterAccountNumber, decimal filterSumTotalDueHigher, decimal filterSumTotalDueLower)
        {
            var paginationFilterModel = new CustomerFilterPagination(skip, take,filterFirstName,filterLastName,filterAccountNumber,filterSumTotalDueHigher,filterSumTotalDueLower);
            var query = _context.Customers
                .Include("Person")
                .Include("SalesOrderHeader")
                .Where(c => c.Person != null);


            if (filterFirstName != null)
            {
                query = query.Where(c => c.Person.FirstName.ToLower().Contains(filterFirstName.ToLower()));
            }

            if (filterLastName != null)
            {
                query = query.Where(c => c.Person.LastName.ToLower().Contains(filterLastName.ToLower()));
            }

            if (filterAccountNumber != null)
            {
                query = query.Where(c => c.AccountNumber.ToLower().Contains(filterAccountNumber.ToString().ToLower()));
            }

            if (filterSumTotalDueHigher != 0)
            {
                query = query.Where(c => c.SalesOrderHeader.Sum(s => s.TotalDue) > filterSumTotalDueHigher); 
            }

            if (filterSumTotalDueLower != 0 )
            {

                query = query.Where(c => c.SalesOrderHeader.Sum(s => s.TotalDue) < filterSumTotalDueLower);
            }


            paginationFilterModel.TotalItems = query.Count();

            paginationFilterModel.CustomerItemList = query.Skip(skip).Take(take).ToList();

            return paginationFilterModel;
        }

      
    }
}
