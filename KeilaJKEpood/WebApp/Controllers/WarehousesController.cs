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
    public class WarehousesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public WarehousesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Warehouses
        public async Task<IActionResult> Index()
        {
            var res =  await _uow.Warehouses.GetAllAsync();

            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: Warehouses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await _uow.Warehouses.FirstOrDefaultAsync(id.Value);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

        // GET: Warehouses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Warehouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Warehouse warehouse)
        {
            if (ModelState.IsValid)
            {
                _uow.Warehouses.Add(warehouse);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(warehouse);
        }

        // GET: Warehouses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await _uow.Warehouses.FirstOrDefaultAsync(id.Value);
            if (warehouse == null)
            {
                return NotFound();
            }
            return View(warehouse);
        }

        // POST: Warehouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Warehouse warehouse)
        {
            if (id != warehouse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Warehouses.Update(warehouse);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await WarehouseExists(warehouse.Id))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(warehouse);
        }

        // GET: Warehouses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warehouse = await _uow.Warehouses.FirstOrDefaultAsync(id.Value);
            if (warehouse == null)
            {
                return NotFound();
            }

            return View(warehouse);
        }

        // POST: Warehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.Warehouses.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> WarehouseExists(Guid id)
        {
            return await _uow.Warehouses.ExistsAsync(id);
        }
    }
}
