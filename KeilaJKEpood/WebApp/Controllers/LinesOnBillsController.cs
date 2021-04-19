using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using BLL.App.DTO;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using WebApp.Helpers;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize]
    public class LinesOnBillsController : Controller
    {
        private readonly IAppBLL _bll;

        public LinesOnBillsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: LinesOnBills
        public async Task<IActionResult> Index()
        {
            var res =  await _bll.LinesOnBills.GetAllAsync(User.GetUserId()!.Value);

            await _bll.SaveChangesAsync();
            return View(res);
        }

        // GET: LinesOnBills/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineOnBill = await _bll.LinesOnBills.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
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
                _bll.LinesOnBills.Add(lineOnBill);
                await _bll.SaveChangesAsync();
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

            var lineOnBill = await _bll.LinesOnBills.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
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
                    _bll.LinesOnBills.Update(lineOnBill);
                    await _bll.SaveChangesAsync();
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

            var lineOnBill = await _bll.LinesOnBills.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
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
            await _bll.LinesOnBills.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> LineOnBillExists(Guid id)
        {
            return await _bll.LinesOnBills.ExistsAsync(id, User.GetUserId()!.Value);
        }
    }
}
