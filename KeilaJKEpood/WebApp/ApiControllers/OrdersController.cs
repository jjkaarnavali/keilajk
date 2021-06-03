using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Order = DTO.App.OrderDTO;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for OrdersController
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public OrdersController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
        }

        // GET: api/Orders
        /// <summary>
        /// Get all the Orders
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Order>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return Ok(await _bll.Orders.GetAllAsync());
        }

        // GET: api/Orders/5
        /// <summary>
        /// Get one Order. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>BLL.App.DTO.Order</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {

            var orders = await _bll.Orders.GetAllAsync();
            var order = await _bll.Orders.FirstOrDefaultAsync(id);

            foreach (var ord in orders)
            {
                if (ord.Id == id)
                {
                    order = ord;
                } 
            }
            
            
            if (order == null)
            {
                return NotFound();
            }

            return Mapper.Map(order, new DTO.App.OrderDTO());
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update a Order thats already in the DB
        /// </summary>
        /// <param name="id">Id of the Order</param>
        /// <param name="order">The updated Order</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutOrder(Guid id, DTO.App.OrderAdd order)
        {
            var orders = await _bll.Orders.GetAllAsync();
            foreach (var ord in orders)
            {
                if (ord.Id == Guid.Parse(order.id))
                {
                    ord.Until = DateTime.Now;
                    
                    _bll.Orders.Update(ord);
            
                    await _bll.SaveChangesAsync();
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a new Order
        /// </summary>
        /// <param name="order">Entity of type BLL.App.DTO.Order</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _bll.Orders.Add(Mapper.Map(order, new BLL.App.DTO.Order()));
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        /// <summary>
        /// Delete a Order from the DB.
        /// </summary>
        /// <param name="id">Id of the Order to be deleted.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _bll.Orders.FirstOrDefaultAsync(id);
            
            if (order == null)
            {
                return NotFound();
            }

            _bll.Orders.Remove(order);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
