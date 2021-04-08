using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using BLL.App.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using ProductInWarehouse = BLL.App.DTO.ProductInWarehouse;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsInWarehousesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ProductsInWarehousesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ProductsInWarehouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductInWarehouse>>> GetProductsInWarehouses()
        {
            return Ok(await _bll.ProductsInWarehouses.GetAllAsync());
        }

        // GET: api/ProductsInWarehouses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductInWarehouse>> GetProductInWarehouse(Guid id)
        {
            var productInWarehouse = await _bll.ProductsInWarehouses.FirstOrDefaultAsync(id);

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

            _bll.ProductsInWarehouses.Update(productInWarehouse);
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ProductsInWarehouses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductInWarehouse>> PostProductInWarehouse(ProductInWarehouse productInWarehouse)
        {
            _bll.ProductsInWarehouses.Add(productInWarehouse);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetProductInWarehouse", new { id = productInWarehouse.Id }, productInWarehouse);
        }

        // DELETE: api/ProductsInWarehouses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductInWarehouse(Guid id)
        {
            var productInWarehouse = await _bll.ProductsInWarehouses.FirstOrDefaultAsync(id);
            if (productInWarehouse == null)
            {
                return NotFound();
            }

            _bll.ProductsInWarehouses.Remove(productInWarehouse);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
