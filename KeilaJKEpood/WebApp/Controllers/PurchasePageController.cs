using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO;
using Contracts.BLL.App;
using Domain.App;
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

    public class PurchasePageController : Controller
    {


        private readonly IAppBLL _bll;

        public PurchasePageController(IAppBLL bll)
        {
            _bll = bll;
        }

        public async Task<IActionResult> Index(Guid personId)
        {
           // Create bill
           var orders = await _bll.Orders.GetAllAsync(User.GetUserId()!.Value);
           var userId = User.GetUserId();
           BLL.App.DTO.Bill bill = new BLL.App.DTO.Bill();
           bill.Id = Guid.NewGuid();
           bill.PersonId = personId;
           bill.UserId = (Guid) userId;
           BLL.App.DTO.Order currentOrder = new BLL.App.DTO.Order();
           foreach (var order in orders)
           {
               if (order.UserId == userId && order.Until == null)
               {
                   bill.OrderId = order.Id;
                   currentOrder = order;
               }
           }

           bill.BillNr = bill.Id.ToString();
           bill.CreationTime = DateTime.Now;
           bill.PriceToPay = 0;
           bill.PriceWithoutTax = 0;
           bill.SumOfTax = 0;
           
           
           
           // Create lines on bill
           var productsInOrders = await _bll.ProductsInOrders.GetAllAsync(User.GetUserId()!.Value);

           foreach (var productInOrder in productsInOrders)
           {
               if (productInOrder.OrderId == currentOrder.Id && productInOrder.Until == null)
               {
                   BLL.App.DTO.LineOnBill lineOnBill = new BLL.App.DTO.LineOnBill();
                   lineOnBill.Id = Guid.NewGuid();
                   lineOnBill.BillId = bill.Id;
                   lineOnBill.ProductId = productInOrder.ProductId;
                   lineOnBill.Amount = productInOrder.ProductAmount;
                   lineOnBill.PriceId = await GetPriceId(lineOnBill.ProductId);
                   lineOnBill.TaxPercentage = (decimal) 0.2;
                   lineOnBill.PriceWithoutTax = await GetPrice(lineOnBill.ProductId) * lineOnBill.Amount;
                   lineOnBill.PriceToPay = lineOnBill.PriceWithoutTax * lineOnBill.TaxPercentage +
                                           lineOnBill.PriceWithoutTax;
                   lineOnBill.SumOfTax = lineOnBill.PriceToPay - lineOnBill.PriceWithoutTax;
                   
                   _bll.LinesOnBills.Add(lineOnBill);
                   await _bll.SaveChangesAsync();
               }
           }
           // update bill prices
           var linesOnBill = await _bll.LinesOnBills.GetAllAsync(User.GetUserId()!.Value);
           decimal priceNoTax = 0;
           decimal priceTotal = 0;
           decimal taxTotal = 0;
           foreach (var line in linesOnBill)
           {
               if (line.BillId == bill.Id)
               {
                   priceNoTax += line.PriceWithoutTax;
                   priceTotal += line.PriceToPay;
                   taxTotal += line.SumOfTax;
               }
           }

           bill.PriceWithoutTax = priceNoTax;
           bill.PriceToPay = priceTotal;
           bill.SumOfTax = taxTotal;
           
           _bll.Bills.Add(bill);
           await _bll.SaveChangesAsync();
           
           
           // ask for payment method
           
           
           return View(await _bll.PaymentTypes.GetAllAsync(User.GetUserId()!.Value));
        }
        

        public async Task<Guid> GetPriceId(Guid id)
        {
            var prices = await _bll.Prices.GetAllAsync(User.GetUserId()!.Value);
            foreach (var price in prices)
            {
                if (price.ProductId == id)
                {
                    return price.Id;
                }
            }

            return Guid.Empty;
        }
        
        public async Task<decimal> GetPrice(Guid id)
        {
            var prices = await _bll.Prices.GetAllAsync(User.GetUserId()!.Value);
            foreach (var price in prices)
            {
                if (price.ProductId == id)
                {
                    return price.PriceInEur;
                }
            }

            return 0;
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