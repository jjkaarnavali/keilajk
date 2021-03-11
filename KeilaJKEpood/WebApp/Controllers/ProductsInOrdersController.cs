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
    public class ProductsInOrdersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProductInOrderRepository _repository;

        public ProductsInOrdersController(AppDbContext context)
        {
            _context = context;
            _repository = new ProductInOrderRepository(_context);
        }

        // GET: ProductsInOrders
        public async Task<IActionResult> Index()
        {
            var res =  await _repository.GetAllAsync();
            return View(res);
        }

        // GET: ProductsInOrders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInOrder = await _context.ProductsInOrders
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create([Bind("Id,ProductId,OrderId,ProductAmount,From,Until")] ProductInOrder productInOrder)
        {
            if (ModelState.IsValid)
            {
                productInOrder.Id = Guid.NewGuid();
                _context.Add(productInOrder);
                await _context.SaveChangesAsync();
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

            var productInOrder = await _context.ProductsInOrders.FindAsync(id);
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ProductId,OrderId,ProductAmount,From,Until")] ProductInOrder productInOrder)
        {
            if (id != productInOrder.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productInOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductInOrderExists(productInOrder.Id))
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
            return View(productInOrder);
        }

        // GET: ProductsInOrders/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInOrder = await _context.ProductsInOrders
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var productInOrder = await _context.ProductsInOrders.FindAsync(id);
            _context.ProductsInOrders.Remove(productInOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductInOrderExists(Guid id)
        {
            return _context.ProductsInOrders.Any(e => e.Id == id);
        }
    }
}
