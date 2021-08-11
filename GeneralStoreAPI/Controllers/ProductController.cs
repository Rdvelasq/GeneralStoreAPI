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
    public class ProductController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Null Value");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Products.Add(product);
            if (await _context.SaveChangesAsync() > 0)
            {
                return Ok("Product Saved");
            }
            return InternalServerError();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetBySKU([FromUri] string SKU)
        {
            Product product =  await _context.Products.FindAsync(SKU);
            if (product == null)
            {
                return BadRequest("Product was not found");
            }
            return Ok(product);
        }
    }
}
