using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LinesOnBillsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public LinesOnBillsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/LinesOnBills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LineOnBill>>> GetLinesOnBills()
        {
            return Ok(await _uow.LinesOnBills.GetAllAsync());
        }

        // GET: api/LinesOnBills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LineOnBill>> GetLineOnBill(Guid id)
        {
            var lineOnBill = await _uow.LinesOnBills.FirstOrDefaultAsync(id);

            if (lineOnBill == null)
            {
                return NotFound();
            }

            return lineOnBill;
        }

        // PUT: api/LinesOnBills/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLineOnBill(Guid id, LineOnBill lineOnBill)
        {
            if (id != lineOnBill.Id)
            {
                return BadRequest();
            }

            _uow.LinesOnBills.Update(lineOnBill);
            
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/LinesOnBills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LineOnBill>> PostLineOnBill(LineOnBill lineOnBill)
        {
            _uow.LinesOnBills.Add(lineOnBill);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetLineOnBill", new { id = lineOnBill.Id }, lineOnBill);
        }

        // DELETE: api/LinesOnBills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLineOnBill(Guid id)
        {
            var lineOnBill = await _uow.LinesOnBills.FirstOrDefaultAsync(id);
            if (lineOnBill == null)
            {
                return NotFound();
            }

            _uow.LinesOnBills.Remove(lineOnBill);
            await _uow.SaveChangesAsync();

            return NoContent();
        }
    }
}
