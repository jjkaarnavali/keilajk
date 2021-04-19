using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using PaymentType = BLL.App.DTO.PaymentType;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for PaymentTypesController
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentTypesController : ControllerBase
    {
        
        private readonly IAppBLL _bll;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public PaymentTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/PaymentTypes
        /// <summary>
        /// Get all PaymentTypes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<PaymentType>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PaymentType>>> GetPaymentTypes()
        {
            return Ok(await _bll.PaymentTypes.GetAllAsync());
        }

        // GET: api/PaymentTypes/5
        /// <summary>
        /// Get one PaymentType. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>BLL.App.DTO.PaymentType</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PaymentType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PaymentType>> GetPaymentType(Guid id)
        {
            var paymentType = await _bll.PaymentTypes.FirstOrDefaultAsync(id);

            if (paymentType == null)
            {
                return NotFound();
            }

            return paymentType;
        }

        // PUT: api/PaymentTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update a PaymentType thats already in the DB
        /// </summary>
        /// <param name="id">Id of the PaymentType</param>
        /// <param name="paymentType">The updated PaymentType</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PaymentType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutPaymentType(Guid id, PaymentType paymentType)
        {
            if (id != paymentType.Id)
            {
                return BadRequest();
            }

            _bll.PaymentTypes.Update(paymentType);
            
            await _bll.SaveChangesAsync();
            
            

            return NoContent();
        }

        // POST: api/PaymentTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a new PaymentType
        /// </summary>
        /// <param name="paymentType">Entity of type BLL.App.DTO.PaymentType</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PaymentType), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<PaymentType>> PostPaymentType(PaymentType paymentType)
        {
            _bll.PaymentTypes.Add(paymentType);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPaymentType", new { id = paymentType.Id }, paymentType);
        }

        // DELETE: api/PaymentTypes/5
        /// <summary>
        /// Delete a PaymentType from the DB.
        /// </summary>
        /// <param name="id">Id of the PaymentType to be deleted.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeletePaymentType(Guid id)
        {
            
            var paymentType = await _bll.PaymentTypes.FirstOrDefaultAsync(id);
            if (paymentType == null)
            {
                return NotFound();
            }

            _bll.PaymentTypes.Remove(paymentType);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        
    }
}
