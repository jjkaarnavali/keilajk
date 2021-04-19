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
    public class PricesController : Controller
    {
        private readonly IAppBLL _bll;

        public PricesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: Prices
        public async Task<IActionResult> Index()
        {
            var res =  await _bll.Prices.GetAllAsync(User.GetUserId()!.Value);

            await _bll.SaveChangesAsync();
            return View(res);
        }

        // GET: Prices/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _bll.Prices.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // GET: Prices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Price price)
        {
            if (ModelState.IsValid)
            {
                _bll.Prices.Add(price);
                await _bll.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(price);
        }

        // GET: Prices/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _bll.Prices.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (price == null)
            {
                return NotFound();
            }
            return View(price);
        }

        // POST: Prices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Price price)
        {
            if (id != price.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bll.Prices.Update(price);
                    await _bll.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PriceExists(price.Id))
                    {
                        return NotFound();
                    }
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(price);
        }

        // GET: Prices/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var price = await _bll.Prices.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (price == null)
            {
                return NotFound();
            }

            return View(price);
        }

        // POST: Prices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _bll.Prices.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PriceExists(Guid id)
        {
            return await _bll.Prices.ExistsAsync(id, User.GetUserId()!.Value);
        }
    }
}
