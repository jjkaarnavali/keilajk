using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using ProductType = BLL.App.DTO.ProductType;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for ProductTypes
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductTypesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ProductTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ProductTypes
        /// <summary>
        /// Get all ProductTypes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<ProductType>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetProductTypes()
        {
            return Ok(await _bll.ProductTypes.GetAllAsync());
        }

        // GET: api/ProductTypes/5
        /// <summary>
        /// Get one ProductType. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>BLL.App.DTO.ProductType</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        /// <summary>
        /// Update a ProductType thats already in the DB
        /// </summary>
        /// <param name="id">Id of the ProductType</param>
        /// <param name="productType">The updated ProductType</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        /// <summary>
        /// Add a new ProductType
        /// </summary>
        /// <param name="productType">Entity of type BLL.App.DTO.ProductType</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ProductType>> PostProductType(DTO.App.ProductTypeAdd productType)
        {
            var bllProductType = new ProductType()
            {
                TypeName = productType.TypeName
            };
            var addedProductType = _bll.ProductTypes.Add(bllProductType);
            
            // bll will call dal.SaveChangesAsync => will call EF.SaveChangesAsync()
            // ef will update entities with new ID-s
            await _bll.SaveChangesAsync();

            var returnProductType = new DTO.App.ProductTypeDTO()
            {
                Id = addedProductType.Id,
                TypeName = addedProductType.TypeName
            };

            return CreatedAtAction("GetProductType", new {id = returnProductType.Id}, returnProductType);
        }

        // DELETE: api/ProductTypes/5
        /// <summary>
        /// Delete a ProductType from the DB.
        /// </summary>
        /// <param name="id">Id of the ProductType to be deleted.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
