using FullStack.DAL.Interfaces;
using FullStack.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using FullStack.DAL.Models.Entities;
using Microsoft.EntityFrameworkCore.Internal;

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
            var paginationFilterModel = new CustomerFilterPagination(skip, take);
            var query = _context.Customers
                .Include("Person")
                .Include("SalesOrderHeader")
                .Where(c => c.Person != null);


            if (!string.IsNullOrEmpty(filterFirstName) && filterFirstName != "null")
            {
                paginationFilterModel.FilterFirstName = filterFirstName;
                query = query.Where(c => c.Person.FirstName.ToLower().Contains(filterFirstName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filterLastName) && filterLastName != "null")
            {
                paginationFilterModel.FilterLastName = filterLastName;
                query = query.Where(c => c.Person.LastName.ToLower().Contains(filterLastName.ToLower()));
            }

            if (!string.IsNullOrEmpty(filterAccountNumber ) && filterAccountNumber != "null")
            {
                paginationFilterModel.FilterAccountNumber = filterAccountNumber;
                query = query.Where(c => c.AccountNumber.ToLower().Contains(filterAccountNumber.ToString().ToLower()));
            }

            if (filterSumTotalDueHigher != 0)
            {
                paginationFilterModel.FilterSumTotalDueHigher = filterSumTotalDueHigher;
                query = query.Where(c => c.SalesOrderHeader.Sum(s => s.TotalDue) > filterSumTotalDueHigher); 
            }

            if (filterSumTotalDueLower != 0 )
            {
                paginationFilterModel.FilterSumTotalDueLower = filterSumTotalDueLower;
                query = query.Where(c => c.SalesOrderHeader.Sum(s => s.TotalDue) < filterSumTotalDueLower);
            }


            paginationFilterModel.TotalItems = query.Count();

            paginationFilterModel.CustomerItemList = query.Skip(skip).Take(take).ToList();

            return paginationFilterModel;
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }

        public Customer GetCustomer(int customerId)
        {
            return  _context.Customers.Include(c=>c.Person).AsNoTracking().FirstOrDefault(d=>d.CustomerId ==customerId);
        }

      
    }
}
