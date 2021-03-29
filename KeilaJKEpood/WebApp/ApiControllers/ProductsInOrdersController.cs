using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsInOrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsInOrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductsInOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductInOrder>>> GetProductsInOrders()
        {
            return await _context.ProductsInOrders.ToListAsync();
        }

        // GET: api/ProductsInOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductInOrder>> GetProductInOrder(Guid id)
        {
            var productInOrder = await _context.ProductsInOrders.FindAsync(id);

            if (productInOrder == null)
            {
                return NotFound();
            }

            return productInOrder;
        }

        // PUT: api/ProductsInOrders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductInOrder(Guid id, ProductInOrder productInOrder)
        {
            if (id != productInOrder.Id)
            {
                return BadRequest();
            }

            _context.Entry(productInOrder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductInOrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductsInOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductInOrder>> PostProductInOrder(ProductInOrder productInOrder)
        {
            _context.ProductsInOrders.Add(productInOrder);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductInOrder", new { id = productInOrder.Id }, productInOrder);
        }

        // DELETE: api/ProductsInOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductInOrder(Guid id)
        {
            var productInOrder = await _context.ProductsInOrders.FindAsync(id);
            if (productInOrder == null)
            {
                return NotFound();
            }

            _context.ProductsInOrders.Remove(productInOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductInOrderExists(Guid id)
        {
            return _context.ProductsInOrders.Any(e => e.Id == id);
        }
    }
}
