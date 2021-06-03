using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Product = DTO.App.ProductDTO;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for Products
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public ProductsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
        }

        // GET: api/Products
        /// <summary>
        /// Get all Products
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return Ok(await _bll.Products.GetAllAsync());
        }

        // GET: api/Products/5
        /// <summary>
        /// Get one Product. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>BLL.App.DTO.Product</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Product>> GetProduct(Guid id)
        {
            var products = await _bll.Products.GetAllAsync();
            var product = await _bll.Products.FirstOrDefaultAsync(id);

            foreach (var prod in products)
            {
                if (prod.Id == id)
                {
                    product = prod;
                } 
            }
            
            
            if (product == null)
            {
                return NotFound();
            }

            return Mapper.Map(product, new DTO.App.ProductDTO());
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update a Product thats already in the DB
        /// </summary>
        /// <param name="id">Id of the Product</param>
        /// <param name="product">The updated Product</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutProduct(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _bll.Products.Update(Mapper.Map(product, new BLL.App.DTO.Product()));
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a new Product
        /// </summary>
        /// <param name="product">Entity of type BLL.App.DTO.Product</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _bll.Products.Add(Mapper.Map(product, new BLL.App.DTO.Product()));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        /// <summary>
        /// Delete a Product from the DB.
        /// </summary>
        /// <param name="id">Id of the Product to be deleted.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _bll.Products.FirstOrDefaultAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _bll.Products.Remove(product);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
