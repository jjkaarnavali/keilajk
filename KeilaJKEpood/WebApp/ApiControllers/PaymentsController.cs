using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Payment = BLL.App.DTO.Payment;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for PaymentsController
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public PaymentsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Payments
        /// <summary>
        /// Get all Payments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Payment>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            return Ok(await _bll.Payments.GetAllAsync());
        }

        // GET: api/Payments/5
        /// <summary>
        /// Get one Payment. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>BLL.App.DTO.Payment</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Payment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Payment>> GetPayment(Guid id)
        {
            var payment = await _bll.Payments.FirstOrDefaultAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }

        // PUT: api/Payments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update a Payment thats already in the DB
        /// </summary>
        /// <param name="id">Id of the Payment</param>
        /// <param name="payment">The updated Payment</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Payment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutPayment(Guid id, Payment payment)
        {
            if (id != payment.Id)
            {
                return BadRequest();
            }

            _bll.Payments.Update(payment);
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Payments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a new Payment
        /// </summary>
        /// <param name="payment">Entity of type BLL.App.DTO.Payment</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Payment), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Payment>> PostPayment(DTO.App.PaymentAdd payment)
        {
            var bllPayment = new Payment()
            {
                PaymentTypeId = Guid.Parse(payment.PaymentTypeId),
                BillId = Guid.Parse(payment.BillId),
                PersonId = Guid.Parse(payment.PersonId),
            };
            bllPayment.Id = Guid.NewGuid();
            bllPayment.PaymentTime = DateTime.Now;
            _bll.Payments.Add(bllPayment);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPayment", new { id = bllPayment.Id }, payment);
        }

        // DELETE: api/Payments/5
        /// <summary>
        /// Delete a Payment from the DB.
        /// </summary>
        /// <param name="id">Id of the Payment to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeletePayment(Guid id)
        {
            var payment = await _bll.Payments.FirstOrDefaultAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            _bll.Payments.Remove(payment);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
