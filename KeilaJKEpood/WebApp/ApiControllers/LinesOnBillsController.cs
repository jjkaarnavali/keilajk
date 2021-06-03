using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using DTO.App.MappingProfiles;
using LineOnBill = DTO.App.LineOnBillDTO;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API controller for LinesOnBills
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LinesOnBillsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly IMapper Mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public LinesOnBillsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
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
            

            return Mapper.Map(lineOnBill, new DTO.App.LineOnBillDTO());
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

            var bllLineOnBill = new BLL.App.DTO.LineOnBill();
            bllLineOnBill = Mapper.Map(lineOnBill, new BLL.App.DTO.LineOnBill());

            _bll.LinesOnBills.Update(bllLineOnBill);
            
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
        public async Task<ActionResult<LineOnBill>> PostLineOnBill(DTO.App.LineOnBillAdd lineOnBill)
        {
            var bllLineOnBill = new BLL.App.DTO.LineOnBill()
            {
                BillId = Guid.Parse(lineOnBill.BillId),
                PriceId = Guid.Parse(lineOnBill.PriceId),
                ProductId = Guid.Parse(lineOnBill.ProductId),
                Amount = lineOnBill.Amount,
                TaxPercentage = lineOnBill.TaxPercentage,
                PriceWithoutTax = lineOnBill.PriceWithoutTax,
                SumOfTax = lineOnBill.SumOfTax,
                PriceToPay = lineOnBill.PriceToPay
            };
            bllLineOnBill.Id = Guid.NewGuid();

            _bll.LinesOnBills.Add(bllLineOnBill);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetLineOnBill", new { id = bllLineOnBill.Id }, Mapper.Map(lineOnBill, new DTO.App.LineOnBillDTO()));
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
