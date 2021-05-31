using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using ProductInOrder = BLL.App.DTO.ProductInOrder;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for ProductsInOrders
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsInOrdersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ProductsInOrdersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/ProductsInOrders
        /// <summary>
        /// Get all ProductsInOrders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<ProductInOrder>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<ProductInOrder>>> GetProductsInOrders()
        {
            return Ok(await _bll.ProductsInOrders.GetAllAsync());
        }

        // GET: api/ProductsInOrders/5
        /// <summary>
        /// Get one ProductInOrder. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>BLL.App.DTO.ProductInOrder</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductInOrder), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        /// <summary>
        /// Update a ProductInOrder thats already in the DB
        /// </summary>
        /// <param name="id">Id of the ProductInOrder</param>
        /// <param name="productInOrder">The updated ProductInOrder</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductInOrder), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
        /// <summary>
        /// Add a new ProductInOrder
        /// </summary>
        /// <param name="productInOrder">Entity of type BLL.App.DTO.ProductInOrder</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductInOrder), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ProductInOrder>> PostProductInOrder(ProductInOrder productInOrder)
        {
            _bll.ProductsInOrders.Add(productInOrder);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetProductInOrder", new { id = productInOrder.Id }, productInOrder);
        }

        // DELETE: api/ProductsInOrders/5
        /// <summary>
        /// Delete a ProductInOrder from the DB.
        /// </summary>
        /// <param name="id">Id of the ProductInOrder to be deleted.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
