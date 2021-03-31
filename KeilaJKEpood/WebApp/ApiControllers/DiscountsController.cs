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
    public class DiscountsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public DiscountsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Discounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Discount>>> GetDiscounts()
        {
            return Ok(await _uow.Discounts.GetAllAsync());
        }

        // GET: api/Discounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Discount>> GetDiscount(Guid id)
        {
            var discount = await _uow.Discounts.FirstOrDefaultAsync(id);

            if (discount == null)
            {
                return NotFound();
            }

            return discount;
        }

        // PUT: api/Discounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiscount(Guid id, Discount discount)
        {
            if (id != discount.Id)
            {
                return BadRequest();
            }

            _uow.Discounts.Update(discount);
            
            await _uow.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Discounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Discount>> PostDiscount(Discount discount)
        {
            _uow.Discounts.Add(discount);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetDiscount", new { id = discount.Id }, discount);
        }

        // DELETE: api/Discounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(Guid id)
        {
            var discount = await _uow.Discounts.FirstOrDefaultAsync(id);
            if (discount == null)
            {
                return NotFound();
            }

            _uow.Discounts.Remove(discount);
            await _uow.SaveChangesAsync();

            return NoContent();
        }
    }
}
