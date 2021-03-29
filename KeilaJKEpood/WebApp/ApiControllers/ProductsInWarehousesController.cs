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
    public class ProductsInWarehousesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsInWarehousesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductsInWarehouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductInWarehouse>>> GetProductsInWarehouses()
        {
            return await _context.ProductsInWarehouses.ToListAsync();
        }

        // GET: api/ProductsInWarehouses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductInWarehouse>> GetProductInWarehouse(Guid id)
        {
            var productInWarehouse = await _context.ProductsInWarehouses.FindAsync(id);

            if (productInWarehouse == null)
            {
                return NotFound();
            }

            return productInWarehouse;
        }

        // PUT: api/ProductsInWarehouses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductInWarehouse(Guid id, ProductInWarehouse productInWarehouse)
        {
            if (id != productInWarehouse.Id)
            {
                return BadRequest();
            }

            _context.Entry(productInWarehouse).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductInWarehouseExists(id))
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

        // POST: api/ProductsInWarehouses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductInWarehouse>> PostProductInWarehouse(ProductInWarehouse productInWarehouse)
        {
            _context.ProductsInWarehouses.Add(productInWarehouse);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductInWarehouse", new { id = productInWarehouse.Id }, productInWarehouse);
        }

        // DELETE: api/ProductsInWarehouses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductInWarehouse(Guid id)
        {
            var productInWarehouse = await _context.ProductsInWarehouses.FindAsync(id);
            if (productInWarehouse == null)
            {
                return NotFound();
            }

            _context.ProductsInWarehouses.Remove(productInWarehouse);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductInWarehouseExists(Guid id)
        {
            return _context.ProductsInWarehouses.Any(e => e.Id == id);
        }
    }
}
