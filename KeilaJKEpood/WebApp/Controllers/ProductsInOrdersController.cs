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
    public class ProductsInOrdersController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public ProductsInOrdersController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: ProductsInOrders
        public async Task<IActionResult> Index()
        {
            var res =  await _uow.ProductsInOrders.GetAllAsync();

            await _uow.SaveChangesAsync();
            return View(res);
        }

        // GET: ProductsInOrders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInOrder = await _uow.ProductsInOrders.FirstOrDefaultAsync(id.Value);
            if (productInOrder == null)
            {
                return NotFound();
            }

            return View(productInOrder);
        }

        // GET: ProductsInOrders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductsInOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductInOrder productInOrder)
        {
            if (ModelState.IsValid)
            {
                _uow.ProductsInOrders.Add(productInOrder);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productInOrder);
        }

        // GET: ProductsInOrders/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInOrder = await _uow.ProductsInOrders.FirstOrDefaultAsync(id.Value);
            if (productInOrder == null)
            {
                return NotFound();
            }
            return View(productInOrder);
        }

        // POST: ProductsInOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProductInOrder productInOrder)
        {
            if (id != productInOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.ProductsInOrders.Update(productInOrder);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ProductInOrderExists(productInOrder.Id))
                    {
                        return NotFound();
                    }
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productInOrder);
        }

        // GET: ProductsInOrders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInOrder = await _uow.ProductsInOrders.FirstOrDefaultAsync(id.Value);
            if (productInOrder == null)
            {
                return NotFound();
            }

            return View(productInOrder);
        }

        // POST: ProductsInOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _uow.ProductsInOrders.RemoveAsync(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductInOrderExists(Guid id)
        {
            return await _uow.ProductsInOrders.ExistsAsync(id);
        }
    }
}
