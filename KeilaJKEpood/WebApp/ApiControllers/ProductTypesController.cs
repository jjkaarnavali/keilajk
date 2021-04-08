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
using ProductType = BLL.App.DTO.ProductType;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public ProductTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ProductTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductTypes()
        {
            return Ok(await _bll.ProductTypes.GetAllAsync());
        }

        // GET: api/ProductTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductType>> GetProductType(Guid id)
        {
            var productType = await _bll.ProductTypes.FirstOrDefaultAsync(id);

            if (productType == null)
            {
                return NotFound();
            }

            return productType;
        }

        // PUT: api/ProductTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductType(Guid id, ProductType productType)
        {
            if (id != productType.Id)
            {
                return BadRequest();
            }

            _bll.ProductTypes.Update(productType);
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ProductTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductType>> PostProductType(ProductType productType)
        {
            _bll.ProductTypes.Add(productType);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetProductType", new { id = productType.Id }, productType);
        }

        // DELETE: api/ProductTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductType(Guid id)
        {
            var productType = await _bll.ProductTypes.FirstOrDefaultAsync(id);
            if (productType == null)
            {
                return NotFound();
            }

            _bll.ProductTypes.Remove(productType);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
