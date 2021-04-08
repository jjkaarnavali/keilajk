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
using PaymentType = BLL.App.DTO.PaymentType;

namespace WebApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentTypesController : ControllerBase
    {
        
        private readonly IAppBLL _bll;

        public PaymentTypesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/PaymentTypes
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PaymentType>>> GetPaymentTypes()
        {
            return Ok(await _bll.PaymentTypes.GetAllAsync());
        }

        // GET: api/PaymentTypes/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PaymentType>> GetPaymentType(Guid id)
        {
            var paymentType = await _bll.PaymentTypes.FirstOrDefaultAsync(id);

            if (paymentType == null)
            {
                return NotFound();
            }

            return paymentType;
        }

        // PUT: api/PaymentTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentType(Guid id, PaymentType paymentType)
        {
            if (id != paymentType.Id)
            {
                return BadRequest();
            }

            _bll.PaymentTypes.Update(paymentType);
            
            await _bll.SaveChangesAsync();
            
            

            return NoContent();
        }

        // POST: api/PaymentTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PaymentType>> PostPaymentType(PaymentType paymentType)
        {
            _bll.PaymentTypes.Add(paymentType);
            await _bll.SaveChangesAsync();

            return CreatedAtAction("GetPaymentType", new { id = paymentType.Id }, paymentType);
        }

        // DELETE: api/PaymentTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentType(Guid id)
        {
            
            var paymentType = await _bll.PaymentTypes.FirstOrDefaultAsync(id);
            if (paymentType == null)
            {
                return NotFound();
            }

            _bll.PaymentTypes.Remove(paymentType);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        
    }
}
