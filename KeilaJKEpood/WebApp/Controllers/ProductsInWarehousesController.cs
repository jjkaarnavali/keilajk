using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App.EF;
using DAL.App.EF.Repositories;
using Domain.App;

namespace WebApp.Controllers
{
    public class ProductsInWarehousesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductInWarehouseRepository _repository;

        public ProductsInWarehousesController(AppDbContext context)
        {
            _context = context;
            _repository = new ProductInWarehouseRepository(_context);
        }

        // GET: ProductsInWarehouses
        public async Task<IActionResult> Index()
        {
            var res =  await _repository.GetAllAsync();
            return View(res);
        }

        // GET: ProductsInWarehouses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInWarehouse = await _context.ProductsInWarehouses
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create([Bind("ProductId,WarehouseId,ProductAmount,From,Until,Id")] ProductInWarehouse productInWarehouse)
        {
            if (ModelState.IsValid)
            {
                productInWarehouse.Id = Guid.NewGuid();
                _context.Add(productInWarehouse);
                await _context.SaveChangesAsync();
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

            var productInWarehouse = await _context.ProductsInWarehouses.FindAsync(id);
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
        public async Task<IActionResult> Edit(Guid id, [Bind("ProductId,WarehouseId,ProductAmount,From,Until,Id")] ProductInWarehouse productInWarehouse)
        {
            if (id != productInWarehouse.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productInWarehouse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductInWarehouseExists(productInWarehouse.Id))
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
            return View(productInWarehouse);
        }

        // GET: ProductsInWarehouses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInWarehouse = await _context.ProductsInWarehouses
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var productInWarehouse = await _context.ProductsInWarehouses.FindAsync(id);
            _context.ProductsInWarehouses.Remove(productInWarehouse);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductInWarehouseExists(Guid id)
        {
            return _context.ProductsInWarehouses.Any(e => e.Id == id);
        }
    }
}
