using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Extensions.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApp.Models;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    
    public class CartPageController : Controller
    {
        
        
        private readonly IAppBLL _bll;

        public CartPageController(IAppBLL bll)
        {
            _bll = bll;
        }
        

        public async Task<IActionResult> Index()
        {
            var products = await _bll.Products.GetAllAsync(User.GetUserId()!.Value);
            
            var productsWithPrices = new List<BLL.App.DTO.Product>();
            //var prices = await _bll.Prices.GetAllAsync(User.GetUserId()!.Value);
            
            //CreateOrder();
            
            foreach (var product in products)
            {
                
                var price =  GetPrice(product.Id);
                product.Price = await price;
                
               
                productsWithPrices.Add(product);
            }
            await _bll.SaveChangesAsync();
            return View(productsWithPrices);
        }
        public async Task<decimal> GetPrice(Guid id)
        {
            var prices = await _bll.Prices.GetAllAsync(User.GetUserId()!.Value);
            foreach (var price in prices)
            {
                if (price.ProductId == id)
                {
                    await _bll.SaveChangesAsync();
                    return price.PriceInEur;
                }
            }
            await _bll.SaveChangesAsync();

            return 0;
        }
        

        public async Task<IActionResult> AddToCart(int amount, Guid productId)
        {
            var orders = await _bll.Orders.GetAllAsync(User.GetUserId()!.Value);
            var userId = User.GetUserId();
            var activeOrder = false;
            foreach (var order in orders)
            {
                if (order.UserId == userId && order.Until == null)
                {
                    activeOrder = true;
                }
            }

            if (activeOrder == false)
            {
                var order = new Order();
                order.Id = Guid.NewGuid();
                order.UserId = (Guid) userId;
                order.From = DateTime.Now;
                _bll.Orders.Add(order);
                await _bll.SaveChangesAsync();
                orders = await _bll.Orders.GetAllAsync(User.GetUserId()!.Value);
            }
            
            foreach (var order in orders)
            {
                if (order.UserId == userId && order.Until == null)
                {
                    var itemInOrder = new ProductInOrder();
                    itemInOrder.Id = Guid.NewGuid();
                    itemInOrder.OrderId = order.Id;
                    itemInOrder.ProductAmount = amount;
                    itemInOrder.ProductId = productId;
                    itemInOrder.From = DateTime.Now;
                    _bll.ProductsInOrders.Add(itemInOrder);
                    await _bll.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                    
                }
            }
            return RedirectToAction(nameof(Index));
           
        }
        
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                }
            );

            return LocalRedirect(returnUrl);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}