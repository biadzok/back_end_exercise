using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestApplication.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestApplication.Models;

namespace TestApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly Context _context;

        public ProductController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProduct()
        {

            return await _context.product_table.ToListAsync();
        }

        [HttpGet("{id}")]
        public ActionResult<Products> GetProducts_byId(int id)
        {

            var product_by_id = _context.product_table.ToList().Find(x => x.product_id == id);

            if (product_by_id == null)
            {
                return NotFound();
            }
            return product_by_id;
        }

        [HttpPost]
        public async Task<ActionResult<Products>> Add_Products(Products product)
        {// the products are created in the table but it makes an error after creating it...
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            await _context.product_table.AddAsync(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.product_id }, product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Products>> Delete_Product(int id)
        {
            var product = _context.product_table.Find(id);

            if (product == null)
            {
                return NotFound();
            }
            else
            {
                _context.Remove(product);
                await _context.SaveChangesAsync();
                return product;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update_Products(int id, Products product)
        {
            if (id != product.product_id || !_context.product_table.Any(x => x.product_id == id))
            {
                return BadRequest();
            }
            else
            {
                var products = _context.product_table.SingleOrDefault(x => x.product_id == id);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }
    }
}