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

namespace WebApp.Controllers
{
    [Authorize]
    public class LinesOnBillsController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public LinesOnBillsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: LinesOnBills
        public async Task<IActionResult> Index()
        {
            var res =  await _uow.LinesOnBills.GetAllAsync();

            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: LinesOnBills/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineOnBill = await _uow.LinesOnBills.FirstOrDefaultAsync(id.Value);
            if (lineOnBill == null)
            {
                return NotFound();
            }

            return View(lineOnBill);
        }

        // GET: LinesOnBills/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LinesOnBills/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LineOnBill lineOnBill)
        {
            if (ModelState.IsValid)
            {
                _uow.LinesOnBills.Add(lineOnBill);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lineOnBill);
        }

        // GET: LinesOnBills/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineOnBill = await _uow.LinesOnBills.FirstOrDefaultAsync(id.Value);
            if (lineOnBill == null)
            {
                return NotFound();
            }
            return View(lineOnBill);
        }

        // POST: LinesOnBills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, LineOnBill lineOnBill)
        {
            if (id != lineOnBill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.LinesOnBills.Update(lineOnBill);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await LineOnBillExists(lineOnBill.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(lineOnBill);
        }

        // GET: LinesOnBills/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineOnBill = await _uow.LinesOnBills.FirstOrDefaultAsync(id.Value);
            if (lineOnBill == null)
            {
                return NotFound();
            }

            return View(lineOnBill);
        }

        // POST: LinesOnBills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.LinesOnBills.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> LineOnBillExists(Guid id)
        {
            return await _uow.LinesOnBills.ExistsAsync(id);
        }
    }
}
