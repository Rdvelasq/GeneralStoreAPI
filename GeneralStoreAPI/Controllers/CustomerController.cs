using GeneralStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    public class CustomerController : ApiController
    {
        //Controller is tje entry point of an API call
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Customer customer)
        {
            if (customer is null)
            {
                return BadRequest("Null value Error");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Customers.Add(customer);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok($"{customer.FullName} has been saved");
            }
            return InternalServerError();
        }
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            List<Customer> cusomters = await _context.Customers.ToListAsync();
            return Ok(cusomters);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetByID([FromUri] int ID)
        {
            Customer customer = await _context.Customers.FindAsync(ID);
            if (customer is null)
            {
                return BadRequest("Unable to Locate Customer");
            }
            else
            {
                return Ok(customer);
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put ([FromUri]int ID, Customer newCustomerData)
        {
            Customer customer = await _context.Customers.FindAsync(ID);
            if(customer is null)
            {
                NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            customer.FirstName = newCustomerData.FirstName;
            customer.LastName = newCustomerData.LastName;
            if(await _context.SaveChangesAsync() > 0)
            {
                return Ok("Customer Updateed");
            }

            return InternalServerError();

        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int ID)
        {
            Customer customer = await _context.Customers.FindAsync(ID);
            if(customer != null)
            {
                _context.Customers.Remove(customer);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return Ok($"{customer.FullName} has been deleted");
                }
               
            }
            return InternalServerError();

        }
    }
}
