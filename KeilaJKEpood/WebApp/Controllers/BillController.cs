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
    public class BillController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public BillController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Bill
        public async Task<IActionResult> Index()
        {
            var res =  await _uow.Bills.GetAllAsync(User.GetUserId()!.Value);

            await _uow.SaveChangesAsync();
            
            return View(res);
        }

        // GET: Bill/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _uow.Bills.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // GET: Bill/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Bill/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Bill bill)
        {
            if (ModelState.IsValid)
            {
                _uow.Bills.Add(bill);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bill);
        }

        // GET: Bill/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _uow.Bills.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (bill == null)
            {
                return NotFound();
            }
            return View(bill);
        }

        // POST: Bill/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Bill bill)
        {
            if (id != bill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Bills.Update(bill);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await BillExists(bill.Id))
                    {
                        return NotFound();
                    }
                   
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bill);
        }

        // GET: Bill/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _uow.Bills.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
                
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // POST: Bill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Bills.RemoveAsync(id,User.GetUserId()!.Value);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BillExists(Guid id)
        {
            return await _uow.Bills.ExistsAsync(id, User.GetUserId()!.Value);
        }
    }
}
