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
    public class ProductTypesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ProductTypesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: ProductTypes
        public async Task<IActionResult> Index()
        {
            var res =  await _uow.ProductTypes.GetAllAsync();

            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: ProductTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _uow.ProductTypes.FirstOrDefaultAsync(id.Value);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // GET: ProductTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductType productType)
        {
            if (ModelState.IsValid)
            {
                _uow.ProductTypes.Add(productType);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }

        // GET: ProductTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _uow.ProductTypes.FirstOrDefaultAsync(id.Value);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        // POST: ProductTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductType productType)
        {
            if (id != productType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.ProductTypes.Update(productType);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductTypeExists(productType.Id))
                    {
                        return NotFound();
                    }
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productType);
        }

        // GET: ProductTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _uow.ProductTypes.FirstOrDefaultAsync(id.Value);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // POST: ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.ProductTypes.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductTypeExists(Guid id)
        {
            return await _uow.ProductTypes.ExistsAsync(id);
        }
    }
}
