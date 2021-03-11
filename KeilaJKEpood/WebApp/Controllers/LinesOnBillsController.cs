using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using Domain.App;

namespace WebApp.Controllers
{
    public class LinesOnBillsController : Controller
    {
        private readonly AppDbContext _context;

        public LinesOnBillsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: LinesOnBills
        public async Task<IActionResult> Index()
        {
            return View(await _context.LinesOnBills.ToListAsync());
        }

        // GET: LinesOnBills/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lineOnBill = await _context.LinesOnBills
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create([Bind("Id,BillId,PriceId,ProductId,Amount,TaxPercentage,PriceWithoutTax,SumOfTax,PriceToPay")] LineOnBill lineOnBill)
        {
            if (ModelState.IsValid)
            {
                lineOnBill.Id = Guid.NewGuid();
                _context.Add(lineOnBill);
                await _context.SaveChangesAsync();
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

            var lineOnBill = await _context.LinesOnBills.FindAsync(id);
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,BillId,PriceId,ProductId,Amount,TaxPercentage,PriceWithoutTax,SumOfTax,PriceToPay")] LineOnBill lineOnBill)
        {
            if (id != lineOnBill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lineOnBill);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LineOnBillExists(lineOnBill.Id))
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

            var lineOnBill = await _context.LinesOnBills
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var lineOnBill = await _context.LinesOnBills.FindAsync(id);
            _context.LinesOnBills.Remove(lineOnBill);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LineOnBillExists(Guid id)
        {
            return _context.LinesOnBills.Any(e => e.Id == id);
        }
    }
}
