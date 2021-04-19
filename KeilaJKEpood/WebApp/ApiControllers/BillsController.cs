using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bill = BLL.App.DTO.Bill;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for Bills
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BillsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public BillsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Bills
        /// <summary>
        /// Get all the Bills
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<Bill>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<Bill>>> GetBills()
        {
            return Ok(await _bll.Bills.GetAllAsync());
        }

        /// <summary>
        /// Get one Bill. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>Bill entity from db</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Bill), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Bill>> GetBill(Guid id)
        {
            var bill = await _bll.Bills.FirstOrDefaultAsync(id);

            if (bill == null)
            {
                return NotFound();
            }

            return bill;
        }

        // PUT: api/Bills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update a bill thats already in the DB
        /// </summary>
        /// <param name="id">Id of the bill</param>
        /// <param name="bill">The updated bill</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Bill), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutBill(Guid id, Bill bill)
        {
            if (id != bill.Id)
            {
                return BadRequest();
            }

            _bll.Bills.Update(bill);
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Bills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a new bill
        /// </summary>
        /// <param name="bill">Entity of type BLL.App.DTO.Bill</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(Bill), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<Bill>> PostBill(Bill bill)
        {
            _bll.Bills.Add(bill);
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetBill",
                new
                {
                    id = bill.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                },
                bill);
        }

        // DELETE: api/Bills/5
        /// <summary>
        /// Delete a bill from the DB.
        /// </summary>
        /// <param name="id">Id of the bill to be deleted.</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteBill(Guid id)
        {
            var bill = await _bll.Bills.FirstOrDefaultAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            _bll.Bills.Remove(bill);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        
    }
}
