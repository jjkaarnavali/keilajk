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
    public class ProductsInWarehousesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ProductsInWarehousesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: ProductsInWarehouses
        public async Task<IActionResult> Index()
        {
            var res =  await _uow.ProductsInWarehouses.GetAllAsync(User.GetUserId()!.Value);

            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: ProductsInWarehouses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInWarehouse = await _uow.ProductsInWarehouses.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (productInWarehouse == null)
            {
                return NotFound();
            }

            return View(productInWarehouse);
        }

        // GET: ProductsInWarehouses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductsInWarehouses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductInWarehouse productInWarehouse)
        {
            if (ModelState.IsValid)
            {
                _uow.ProductsInWarehouses.Add(productInWarehouse);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productInWarehouse);
        }

        // GET: ProductsInWarehouses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInWarehouse = await _uow.ProductsInWarehouses.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (productInWarehouse == null)
            {
                return NotFound();
            }
            return View(productInWarehouse);
        }

        // POST: ProductsInWarehouses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductInWarehouse productInWarehouse)
        {
            if (id != productInWarehouse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.ProductsInWarehouses.Update(productInWarehouse);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductInWarehouseExists(productInWarehouse.Id))
                    {
                        return NotFound();
                    }
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productInWarehouse);
        }

        // GET: ProductsInWarehouses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInWarehouse = await _uow.ProductsInWarehouses.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
            if (productInWarehouse == null)
            {
                return NotFound();
            }

            return View(productInWarehouse);
        }

        // POST: ProductsInWarehouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.ProductsInWarehouses.RemoveAsync(id, User.GetUserId()!.Value);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductInWarehouseExists(Guid id)
        {
            return await _uow.ProductsInWarehouses.ExistsAsync(id, User.GetUserId()!.Value);
        }
    }
}
