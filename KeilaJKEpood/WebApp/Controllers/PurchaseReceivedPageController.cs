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

    public class PurchaseReceivedPageController : Controller
    {


        private readonly IAppBLL _bll;

        public PurchaseReceivedPageController(IAppBLL bll)
        {
            _bll = bll;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            // Create payment
            var bills = await _bll.Bills.GetAllAsync(User.GetUserId()!.Value);
            var orders = await _bll.Orders.GetAllAsync(User.GetUserId()!.Value);
            var userId = User.GetUserId();
            BLL.App.DTO.Order orderToPay = new BLL.App.DTO.Order();
            BLL.App.DTO.Bill billToPay = new BLL.App.DTO.Bill();;
            foreach (var order in orders)
            {
                if (order.UserId == userId && order.Until == null)
                {
                    orderToPay = order;
                }
            }

            foreach (var bill in bills)
            {
                if (bill.OrderId == orderToPay.Id)
                {
                    billToPay = bill;
                }
            }
            BLL.App.DTO.Payment payment = new BLL.App.DTO.Payment();
            payment.Id = Guid.NewGuid();
            payment.PaymentTypeId = id;
            payment.BillId = billToPay.Id;
            payment.PersonId = billToPay.PersonId;
            payment.PaymentTime = DateTime.Now;
            _bll.Payments.Add(payment);
            await _bll.SaveChangesAsync();
            
            // Mark orders  as completed
            orderToPay.Until = DateTime.Now;
            _bll.Orders.Update(orderToPay);
            await _bll.SaveChangesAsync();

            return View();
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