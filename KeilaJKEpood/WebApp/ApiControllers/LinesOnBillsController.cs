using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinesOnBillsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LinesOnBillsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/LinesOnBills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LineOnBill>>> GetLinesOnBills()
        {
            return await _context.LinesOnBills.ToListAsync();
        }

        // GET: api/LinesOnBills/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LineOnBill>> GetLineOnBill(Guid id)
        {
            var lineOnBill = await _context.LinesOnBills.FindAsync(id);

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

            _context.Entry(lineOnBill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LineOnBillExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LinesOnBills
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LineOnBill>> PostLineOnBill(LineOnBill lineOnBill)
        {
            _context.LinesOnBills.Add(lineOnBill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLineOnBill", new { id = lineOnBill.Id }, lineOnBill);
        }

        // DELETE: api/LinesOnBills/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLineOnBill(Guid id)
        {
            var lineOnBill = await _context.LinesOnBills.FindAsync(id);
            if (lineOnBill == null)
            {
                return NotFound();
            }

            _context.LinesOnBills.Remove(lineOnBill);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LineOnBillExists(Guid id)
        {
            return _context.LinesOnBills.Any(e => e.Id == id);
        }
    }
}
