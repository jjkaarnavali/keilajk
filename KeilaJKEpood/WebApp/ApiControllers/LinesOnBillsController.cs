using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using LineOnBill = BLL.App.DTO.LineOnBill;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for LinesOnBills
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LinesOnBillsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public LinesOnBillsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/LinesOnBills
        /// <summary>
        /// Get all the LinesOnBills
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<LineOnBill>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<LineOnBill>>> GetLinesOnBills()
        {
            return Ok(await _bll.LinesOnBills.GetAllAsync());
        }

        // GET: api/LinesOnBills/5
        /// <summary>
        /// Get one LineOnBill. Based on parameter: Id
        /// </summary>
        /// <param name="id">Id of object to retrieve, Guid</param>
        /// <returns>BLL.App.DTO.LineOnBill</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(LineOnBill), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<LineOnBill>> GetLineOnBill(Guid id)
        {
            var lineOnBill = await _bll.LinesOnBills.FirstOrDefaultAsync(id);

            if (lineOnBill == null)
            {
                return NotFound();
            }

            return lineOnBill;
        }

        // PUT: api/LinesOnBills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update a LineOnBill thats already in the DB
        /// </summary>
        /// <param name="id">Id of the LineOnBill</param>
        /// <param name="lineOnBill">The updated LineOnBill</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(LineOnBill), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutLineOnBill(Guid id, LineOnBill lineOnBill)
        {
            if (id != lineOnBill.Id)
            {
                return BadRequest();
            }

            _bll.LinesOnBills.Update(lineOnBill);
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/LinesOnBills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Add a new LineOnBill
        /// </summary>
        /// <param name="lineOnBill">Entity of type BLL.App.DTO.LineOnBill</param>
        /// <returns></returns>
        [HttpPost]
        [Consumes("application/json")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(LineOnBill), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<LineOnBill>> PostLineOnBill(LineOnBill lineOnBill)
        {
            _bll.LinesOnBills.Add(lineOnBill);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetLineOnBill", new { id = lineOnBill.Id }, lineOnBill);
        }

        // DELETE: api/LinesOnBills/5
        /// <summary>
        /// Delete a LineOnBill from the DB.
        /// </summary>
        /// <param name="id">Id of the LineOnBill to be deleted</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> DeleteLineOnBill(Guid id)
        {
            var lineOnBill = await _bll.LinesOnBills.FirstOrDefaultAsync(id);
            if (lineOnBill == null)
            {
                return NotFound();
            }

            _bll.LinesOnBills.Remove(lineOnBill);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
