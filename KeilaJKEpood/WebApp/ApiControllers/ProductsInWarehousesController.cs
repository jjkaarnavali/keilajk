using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsInWarehousesController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public ProductsInWarehousesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/ProductsInWarehouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductInWarehouse>>> GetProductsInWarehouses()
        {
            return Ok(await _uow.ProductsInWarehouses.GetAllAsync());
        }

        // GET: api/ProductsInWarehouses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductInWarehouse>> GetProductInWarehouse(Guid id)
        {
            var productInWarehouse = await _uow.ProductsInWarehouses.FirstOrDefaultAsync(id);

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

            _uow.ProductsInWarehouses.Update(productInWarehouse);
            
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ProductsInWarehouses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductInWarehouse>> PostProductInWarehouse(ProductInWarehouse productInWarehouse)
        {
            _uow.ProductsInWarehouses.Add(productInWarehouse);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetProductInWarehouse", new { id = productInWarehouse.Id }, productInWarehouse);
        }

        // DELETE: api/ProductsInWarehouses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductInWarehouse(Guid id)
        {
            var productInWarehouse = await _uow.ProductsInWarehouses.FirstOrDefaultAsync(id);
            if (productInWarehouse == null)
            {
                return NotFound();
            }

            _uow.ProductsInWarehouses.Remove(productInWarehouse);
            await _uow.SaveChangesAsync();

            return NoContent();
        }
    }
}
