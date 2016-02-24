using RestangularGrid.Helpers;
using RestangularGrid.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace RestangularGrid.Controllers
{
    public class CustomersController : ApiController
    {

        private readonly DemoContext db;

        public CustomersController()
        {
            db = new DemoContext();
        }

        // GET: /api/customers
        public PagedResult<Customer> Get(int pageNo = 1, int pageSize = 10, [FromUri] string[] sort = null, string search = null)
        {
            // Determine number of records to skip
            int skip = (pageNo - 1) * pageSize;

            IQueryable<Customer> customerQ = db.Customers;

            // Search
            if (!string.IsNullOrEmpty(search))
            {
                string[] searchElements = search.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach( string ele in searchElements)
                {
                    customerQ = customerQ.Where(c => c.FirstName.Contains(ele) || c.LastName.Contains(ele));
                }
            }

            // Sorting
            if(sort != null)
            {
                customerQ = customerQ.ApplySorting(sort);
            }
            else
            {
                customerQ = customerQ.OrderBy(c => c.LastName); // Default to LastName Order
            }

            // Get the total number of records
            int totalRecordCount = db.Customers.Count();

            // Grab records
            var customers = customerQ
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            // Return page result
            return new PagedResult<Customer>(customers, pageNo, pageSize, totalRecordCount);
        }
    }
}
