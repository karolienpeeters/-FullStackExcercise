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

        public CustomerFilterPagination GetCustomersPage(int skip, int take,string filter)
        {
            var paginationFilterModel = new CustomerFilterPagination(skip, take, filter);
            var query = _context.Customers
                .Include("Person")
                .Include("SalesOrderHeader")
                .Where(c => c.Person != null);

            if (filter != null)
            {
                query = query.Where(c =>
                    c.Person.FirstName.ToLower().Contains(filter.ToLower()) |
                    c.Person.LastName.ToLower().Contains(filter.ToLower()) |
                    c.AccountNumber.ToLower().Contains(filter.ToLower()));
            }

            paginationFilterModel.TotalItems = query.Count();

            paginationFilterModel.CustomerItemList = query.Skip(skip).Take(take).ToList();

            return paginationFilterModel;
        }

      
    }
}
