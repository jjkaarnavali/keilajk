using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bill = DTO.App.BillDTO;

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
        private readonly IMapper Mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public BillsController(IAppBLL bll, IMapper mapper)
        {
            _bll = bll;
            Mapper = mapper;
        }

        // GET: api/Bills
        /// <summary>
        /// Get all the Bills
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<DTO.App.BillDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<DTO.App.BillDTO>>> GetBills()
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
        [ProducesResponseType(typeof(DTO.App.BillDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<DTO.App.BillDTO>> GetBill(Guid id)
        {
            var billBll = await _bll.Bills.FirstOrDefaultAsync(id);

            if (billBll == null)
            {
                return NotFound();
            }

            var bill = new DTO.App.BillDTO()
            {
                Id = billBll.Id,
                PersonId = billBll.PersonId,
                UserId = billBll.UserId,
                OrderId = billBll.OrderId,
                BillNrId = billBll.BillNrId,
                BillNr = billBll.BillNr,
                CreationTime = billBll.CreationTime,
                PriceWithoutTax = billBll.PriceWithoutTax,
                SumOfTax = billBll.SumOfTax,
                PriceToPay = billBll.PriceToPay
            };

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
        [ProducesResponseType(typeof(DTO.App.BillDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> PutBill(string id, DTO.App.BillUpdate bill)
        {
            if (id != bill.Id)
            {
                return BadRequest();
            }

            var bills = await _bll.Bills.GetAllAsync();
            foreach (var billI in bills)
            {
                if (billI.Id == Guid.Parse(bill.Id))
                {
                    billI.PersonId = Guid.Parse(bill.PersonId);
                    billI.UserId = Guid.Parse(bill.UserId);
                    billI.OrderId = Guid.Parse(bill.OrderId);
                    billI.PriceWithoutTax = bill.PriceWithoutTax;
                    billI.SumOfTax = bill.SumOfTax;
                    billI.PriceToPay = bill.PriceToPay;
                    billI.BillNr = bill.Id;
                    billI.CreationTime = DateTime.Now;
                    
                    _bll.Bills.Update(billI);
            
                    await _bll.SaveChangesAsync();
                }
            }

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
        [ProducesResponseType(typeof(DTO.App.BillDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<DTO.App.BillDTO>> PostBill(DTO.App.BillAdd bill)
        {
            
            var bllBill = new Bill()
            {
                PersonId = Guid.Parse(bill.PersonId),
                UserId = Guid.Parse(bill.UserId),
                OrderId = Guid.Parse(bill.OrderId),
                PriceWithoutTax = bill.PriceWithoutTax,
                SumOfTax = bill.SumOfTax,
                PriceToPay = bill.PriceToPay
            };
            bllBill.Id = Guid.NewGuid();
            bllBill.BillNr = bllBill.Id.ToString();
            bllBill.CreationTime = DateTime.Now;
            
            _bll.Bills.Add(Mapper.Map(bllBill, new BLL.App.DTO.Bill()));
            await _bll.SaveChangesAsync();

            return CreatedAtAction(
                "GetBill",
                new
                {
                    id = bllBill.Id,
                    version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "0"
                },
                bllBill);
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
