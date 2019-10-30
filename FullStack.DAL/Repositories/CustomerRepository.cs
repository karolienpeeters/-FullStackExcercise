using FullStack.DAL.Interfaces;
using FullStack.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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
            var paginationFilterModel = new CustomerFilterPagination(skip,take,filter);

          
           if (filter !=null)
           {

               var query = _context.Customers
                   .Include("Person")
                   .Include("SalesOrderHeader")
                   .Where(c => c.Person != null)
                   .Where(c =>
                       c.Person.FirstName.ToLower().Contains(filter.ToLower()) |
                       c.Person.LastName.ToLower().Contains(filter.ToLower()) |
                       c.AccountNumber.ToLower().Contains(filter.ToLower()));

               paginationFilterModel.TotalItems = query.Count();
              
               paginationFilterModel.CustomerItemList = query .Skip(skip)
                   .Take(take).ToList();



            }
            else
           {

              var query= _context.Customers
                   .Include("Person")
                   .Include("SalesOrderHeader")
                   .Where(c => c.Person != null);

              paginationFilterModel.TotalItems = query.Count();


                paginationFilterModel.CustomerItemList = query
                    .Skip(skip)
                    .Take(take).ToList();
            }


           



            return paginationFilterModel;




        }

        public int GetTotalNumberCustomers()
        {

            return _context.Customers

                .Count(m => m.Person != null );
            //return _adventureWorksContext.Customers

            //    .Count(m => m.Person != null && m.Person.FirstName.Contains(filter) || m.Person.LastName.Contains(filter) || m.AccountNumber.Contains(filter));
        }
    }
}
