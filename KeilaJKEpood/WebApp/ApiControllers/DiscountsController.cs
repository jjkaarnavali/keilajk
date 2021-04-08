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
using Discount = BLL.App.DTO.Discount;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DiscountsController : ControllerBase
    {
        private readonly IAppBLL _bll;

        public DiscountsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Discounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Discount>>> GetDiscounts()
        {
            return Ok(await _bll.Discounts.GetAllAsync());
        }

        // GET: api/Discounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Discount>> GetDiscount(Guid id)
        {
            var discount = await _bll.Discounts.FirstOrDefaultAsync(id);

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

            _bll.Discounts.Update(discount);
            
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Discounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Discount>> PostDiscount(Discount discount)
        {
            _bll.Discounts.Add(discount);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetDiscount", new { id = discount.Id }, discount);
        }

        // DELETE: api/Discounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(Guid id)
        {
            var discount = await _bll.Discounts.FirstOrDefaultAsync(id);
            if (discount == null)
            {
                return NotFound();
            }

            _bll.Discounts.Remove(discount);
            await _bll.SaveChangesAsync();

            return NoContent();
        }
    }
}
