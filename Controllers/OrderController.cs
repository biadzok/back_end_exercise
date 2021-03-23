using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApplication.Models;
using TestApplication.Data;
using Microsoft.EntityFrameworkCore;
using TestApplication.DTO;
namespace TestApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        private readonly Context _context;
        public OrderController(Context context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orders>>> GetOrders()
        {
            return await _context.order_table.ToListAsync();
        }

        [HttpGet("id")]
        public async Task<ActionResult<Orders>> GetOrderById(int id)
        {
            var orders = await _context.order_table.FindAsync(id);
            if (orders != null)
            {
                return orders;
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost("Add")]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> AddOrder(OrderDTO request)
        {
            foreach (var item in request.product_id)
            {
                var order = new Orders()
                {
                    customer_id = request.customer_id,
                    product_id = item
                };
                await _context.order_table.AddAsync(order);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction("GetAllOrders", request);
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdateOrder(int id, Orders order)
        {
            if (!id.Equals(order.order_id) || !_context.order_table.Any(x => x.order_id.Equals(id)))
            {
                return BadRequest();
            }
            else
            {
                _context.Entry(order).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetAllOrders", new { id = order.order_id }, order);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Orders>> DeleteOrder(int id)
        {
            var order = await _context.order_table.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.order_table.Remove(order);
            await _context.SaveChangesAsync();

            return order;
        }
    }
}
