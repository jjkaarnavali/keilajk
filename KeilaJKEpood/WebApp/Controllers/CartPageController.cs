using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
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
        
        // https://www.c-sharpcorner.com/UploadFile/ff2f08/multiple-models-in-single-view-in-mvc/
        public async Task<IActionResult> Index()
        {
            
            dynamic mymodel = new ExpandoObject();
            var orders = await _bll.Orders.GetAllAsync(User.GetUserId()!.Value);
            
            var products = await _bll.Products.GetAllAsync(User.GetUserId()!.Value);
            
            var prices = await _bll.Prices.GetAllAsync(User.GetUserId()!.Value);
            
            var productsInOrders = await _bll.ProductsInOrders.GetAllAsync(User.GetUserId()!.Value);

            var userId = User.GetUserId();
            var usersOrder = new Order();
            var usersProductsInOrders = new List<ProductInOrder>();
            var usersProducts = new List<Product>();
            var productPrices = new List<Price>();
            
            // Find the users order
            foreach (var order in orders)
            {
                if (order.UserId == userId && order.Until == null)
                {
                    usersOrder = order;
                }
            }
            
            // Find productsInOrder that are for the order
            foreach (var prInOrder in productsInOrders)
            {
                if (prInOrder.OrderId == usersOrder.Id && prInOrder.Until == null)
                {
                    usersProductsInOrders.Add(prInOrder);
                }
            }
            
            // Find the products themselves
            foreach (var usersPrInOrder in usersProductsInOrders)
            {
                foreach (var product in products)
                {
                    if (usersPrInOrder.ProductId == product.Id)
                    {
                        usersProducts.Add(product);
                    }
                }
            }
            
            // Find the prices of the products
            foreach (var product in products)
            {
                foreach (var price in prices)
                {
                    if (price.ProductId == product.Id && price.Until == null)
                    {
                        productPrices.Add(price);
                    }
                }
            }

            mymodel.productsInOrders = usersProductsInOrders;
            mymodel.products = usersProducts;
            mymodel.prices = productPrices;
            
            
            await _bll.SaveChangesAsync();
            return View(mymodel);
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
        
        public async Task<IActionResult> Remove(Guid id)
        {
            await _bll.ProductsInOrders.RemoveAsync(id, User.GetUserId()!.Value);
            await _bll.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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