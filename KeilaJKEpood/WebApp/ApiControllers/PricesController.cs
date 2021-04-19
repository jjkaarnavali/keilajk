using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Price = BLL.App.DTO.Price;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for Prices
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PricesController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public PricesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Prices
        /// <summary>
        /// Get all Prices.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Price>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<Price>>> GetPrices()
        {
            return Ok(await _bll.Prices.GetAllAsync());
        }

        // GET: api/Prices/5
        /// <summary>
        /// Get one Price. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>BLL.App.DTO.Price</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Price), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Price>> GetPrice(Guid id)
        {
            var price = await _bll.Prices.FirstOrDefaultAsync(id);

            if (price == null)
            {
                return NotFound();
            }

            return price;
        }

        // PUT: api/Prices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update a Price thats already in the DB
        /// </summary>
        /// <param name="id">Id of the Price</param>
        /// <param name="price">The updated Price</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Price), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutPrice(Guid id, Price price)
        {
            if (id != price.Id)
            {
                return BadRequest();
            }

            _bll.Prices.Update(price);
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Prices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a new Price
        /// </summary>
        /// <param name="price">Entity of type BLL.App.DTO.Price</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Price), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Price>> PostPrice(Price price)
        {
            _bll.Prices.Add(price);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPrice", new { id = price.Id }, price);
        }

        // DELETE: api/Prices/5
        /// <summary>
        /// Delete a Price from the DB.
        /// </summary>
        /// <param name="id">Id of the Price to be deleted.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeletePrice(Guid id)
        {
            var price = await _bll.Prices.FirstOrDefaultAsync(id);
            if (price == null)
            {
                return NotFound();
            }

            _bll.Prices.Remove(price);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
