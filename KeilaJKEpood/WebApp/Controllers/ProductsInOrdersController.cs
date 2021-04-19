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
    public class ProductsInOrdersController : Controller
    {
        private readonly IAppBLL _bll;

        public ProductsInOrdersController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: ProductsInOrders
        public async Task<IActionResult> Index()
        {
            var res =  await _bll.ProductsInOrders.GetAllAsync(User.GetUserId()!.Value);

            await _bll.SaveChangesAsync();
            return View(res);
        }

        // GET: ProductsInOrders/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productInOrder = await _bll.ProductsInOrders.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
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
                _bll.ProductsInOrders.Add(productInOrder);
                await _bll.SaveChangesAsync();
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

            var productInOrder = await _bll.ProductsInOrders.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
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
                    _bll.ProductsInOrders.Update(productInOrder);
                    await _bll.SaveChangesAsync();
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

            var productInOrder = await _bll.ProductsInOrders.FirstOrDefaultAsync(id.Value, User.GetUserId()!.Value);
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
            await _bll.ProductsInOrders.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductInOrderExists(Guid id)
        {
            return await _bll.ProductsInOrders.ExistsAsync(id, User.GetUserId()!.Value);
        }
    }
}
