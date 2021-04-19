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
    public class BillController : Controller
    {
        private readonly IAppBLL _bll;

        public BillController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Bill
        public async Task<IActionResult> Index()
        {
            var res =  await _bll.Bills.GetAllAsync(User.GetUserId()!.Value);

            await _bll.SaveChangesAsync();
            return View(res);
        }

        // GET: Bill/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bill = await _bll.Bills.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (bill == null)
            {
                return NotFound();
            }

            return View(bill);
        }

        // GET: Bill/Create
        public IActionResult Create()
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
                _bll.Bills.Add(bill);
                await _bll.SaveChangesAsync();
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

            var bill = await _bll.Bills.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
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
                    _bll.Bills.Update(bill);
                    await _bll.SaveChangesAsync();
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

            var bill = await _bll.Bills.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
                
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
            await _bll.Bills.RemoveAsync(id,User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> BillExists(Guid id)
        {
            return await _bll.Bills.ExistsAsync(id, User.GetUserId()!.Value);
        }
    }
}
