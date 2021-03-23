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
    public class CustomerController : Controller
    {
        private readonly Context _context;
        public CustomerController(Context context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customers>>> GetCustomer()
        {
            return await _context.customer_table.ToListAsync();
        }
        [HttpGet("{id}")]
        public ActionResult<Customers> GetCustomer_byId(int id)
        {
            var customer_by_id = _context.customer_table.ToList().Find(x => x.customer_id == id);

            if (customer_by_id == null)
            {
                return NotFound();
            }
            return customer_by_id;
        }

        [HttpPost]
        public async Task<ActionResult<Customers>> Add_Customers(Customers customer)
        {// the products are created in the table but it makes after creating it...
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            await _context.customer_table.AddAsync(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.customer_id }, customer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customers>> Delete_Customer(int id)
        {
            var customer = _context.customer_table.Find(id);

            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                _context.Remove(customer);
                await _context.SaveChangesAsync();
                return customer;
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update_Customers(int id, Customers customer)
        {
            if (id != customer.customer_id || !_context.customer_table.Any(x => x.customer_id == id))
            {
                return BadRequest();
            }
            else
            {
                var customers = _context.customer_table.SingleOrDefault(x => x.customer_id == id);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }


    }
}
