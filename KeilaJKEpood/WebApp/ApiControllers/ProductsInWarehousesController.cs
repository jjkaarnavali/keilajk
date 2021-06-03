using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using ProductInWarehouse = DTO.App.ProductInWarehouseDTO;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for ProductsInWarehouses
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsInWarehousesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ProductsInWarehousesController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
        }

        // GET: api/ProductsInWarehouses
        /// <summary>
        /// Get all ProductsInWarehouses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<ProductInWarehouse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<ProductInWarehouse>>> GetProductsInWarehouses()
        {
            return Ok(await _bll.ProductsInWarehouses.GetAllAsync());
        }

        // GET: api/ProductsInWarehouses/5
        /// <summary>
        /// Get one ProductInWarehouse. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>BLL.App.DTO.ProductInWarehouse</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductInWarehouse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ProductInWarehouse>> GetProductInWarehouse(Guid id)
        {
            var productInWarehouse = await _bll.ProductsInWarehouses.FirstOrDefaultAsync(id);

            if (productInWarehouse == null)
            {
                return NotFound();
            }

            return Mapper.Map(productInWarehouse, new DTO.App.ProductInWarehouseDTO());
        }

        // PUT: api/ProductsInWarehouses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update a ProductInWarehouse thats already in the DB
        /// </summary>
        /// <param name="id">Id of the ProductInWarehouse</param>
        /// <param name="productInWarehouse">The updated ProductInWarehouse</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductInWarehouse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutProductInWarehouse(Guid id, ProductInWarehouse productInWarehouse)
        {
            if (id != productInWarehouse.Id)
            {
                return BadRequest();
            }

            _bll.ProductsInWarehouses.Update(Mapper.Map(productInWarehouse, new BLL.App.DTO.ProductInWarehouse()));
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/ProductsInWarehouses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a new ProductInWarehouse
        /// </summary>
        /// <param name="productInWarehouse">Entity of type BLL.App.DTO.ProductInWarehouse</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProductInWarehouse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ProductInWarehouse>> PostProductInWarehouse(ProductInWarehouse productInWarehouse)
        {
            _bll.ProductsInWarehouses.Add(Mapper.Map(productInWarehouse, new BLL.App.DTO.ProductInWarehouse()));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetProductInWarehouse", new { id = productInWarehouse.Id }, productInWarehouse);
        }

        // DELETE: api/ProductsInWarehouses/5
        /// <summary>
        /// Delete a ProductInWarehouse from the DB.
        /// </summary>
        /// <param name="id">Id of the ProductInWarehouse to be deleted.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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
