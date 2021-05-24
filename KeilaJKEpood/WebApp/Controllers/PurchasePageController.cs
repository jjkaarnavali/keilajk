using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

    public class PurchasePageController : Controller
    {


        private readonly IAppBLL _bll;

        public PurchasePageController(IAppBLL bll)
        {
            _bll = bll;
        }

        public async Task<IActionResult> Index(decimal totalPrice)
        {
           // Create bill
           
           // Create lines on bill
           
           // ask for payment method
           return View();
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