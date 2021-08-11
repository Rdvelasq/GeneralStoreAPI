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
    public class TransactionController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Transaction transaction)
        {
            Product product = await _context.Products.FindAsync(transaction.ProductSKU);                     

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (product.IsInStock)
            {
                _context.Transactions.Add(transaction);
                product.NumberInInventory--;
                if (await _context.SaveChangesAsync() > 0)
                {
                    return Ok("Transaction recorded");
                }                 
            }
            else
            {
                return BadRequest("Item Not in Stock");
            }
        
            return InternalServerError();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Transaction> transactions = await _context.Transactions.ToListAsync();
            return Ok(transactions);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetByID([FromUri] int ID)
        {
            Transaction transaction = await _context.Transactions.FindAsync(ID);
            if (transaction == null)
            {
                return BadRequest("Unable to Locate Transaction");
            }
            return Ok(transaction);
        }
    }
}
