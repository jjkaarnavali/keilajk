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
using ProductInOrder = BLL.App.DTO.ProductInOrder;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsInOrdersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        
        public ProductsInOrdersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ProductsInOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductInOrder>>> GetProductsInOrders()
        {
            return Ok(await _bll.ProductsInOrders.GetAllAsync());
        }

        // GET: api/ProductsInOrders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductInOrder>> GetProductInOrder(Guid id)
        {
            var productInOrder = await _bll.ProductsInOrders.FirstOrDefaultAsync(id);

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

            _bll.ProductsInOrders.Update(productInOrder);
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ProductsInOrders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductInOrder>> PostProductInOrder(ProductInOrder productInOrder)
        {
            _bll.ProductsInOrders.Add(productInOrder);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetProductInOrder", new { id = productInOrder.Id }, productInOrder);
        }

        // DELETE: api/ProductsInOrders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductInOrder(Guid id)
        {
            var productInOrder = await _bll.ProductsInOrders.FirstOrDefaultAsync(id);
            if (productInOrder == null)
            {
                return NotFound();
            }

            _bll.ProductsInOrders.Remove(productInOrder);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
