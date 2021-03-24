using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain.App;
using Microsoft.AspNetCore.Authorization;
using WebApp.Helpers;

namespace WebApp.Controllers
{
    
    [Authorize]
    public class PaymentTypesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PaymentTypesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: PaymentTypes
        public async Task<IActionResult> Index()
        {
            var res =  await _uow.PaymentTypes.GetAllAsync(User.GetUserId()!.Value);

            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: PaymentTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = await _uow.PaymentTypes.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (paymentType == null)
            {
                return NotFound();
            }

            return View(paymentType);
        }

        // GET: PaymentTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PaymentType paymentType)
        {
            if (ModelState.IsValid)
            {
                _uow.PaymentTypes.Add(paymentType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentType);
        }

        // GET: PaymentTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = await _uow.PaymentTypes.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (paymentType == null)
            {
                return NotFound();
            }
            return View(paymentType);
        }

        // POST: PaymentTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, PaymentType paymentType)
        {
            if (id != paymentType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.PaymentTypes.Update(paymentType);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PaymentTypeExists(paymentType.Id))
                    {
                        return NotFound();
                    }
                   
                }
                return RedirectToAction(nameof(Index));
            }
            return View(paymentType);
        }

        // GET: PaymentTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentType = await _uow.PaymentTypes.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (paymentType == null)
            {
                return NotFound();
            }

            return View(paymentType);
        }

        // POST: PaymentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.PaymentTypes.RemoveAsync(id, User.GetUserId()!.Value);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PaymentTypeExists(Guid id)
        {
            return await _uow.PaymentTypes.ExistsAsync(id, User.GetUserId()!.Value);
        }
    }
}
