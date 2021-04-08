using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using BLL.App.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Bill = BLL.App.DTO.Bill;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BillsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public BillsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Bills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bill>>> GetBills()
        {
            return Ok(await _bll.Bills.GetAllAsync());
        }

        // GET: api/Bills/5
        [HttpGet("{id}")]
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
        [HttpPut("{id}")]
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
        [HttpPost]
        public async Task<ActionResult<Bill>> PostBill(Bill bill)
        {
            _bll.Bills.Add(bill);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetBill", new { id = bill.Id }, bill);
        }

        // DELETE: api/Bills/5
        [HttpDelete("{id}")]
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
