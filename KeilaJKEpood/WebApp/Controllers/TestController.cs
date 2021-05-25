using System.Threading.Tasks;
using DAL.App.EF;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.ViewModels.Test;

namespace WebApp.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger<TestController> _logger;
        private readonly AppDbContext _ctx;
        
        public TestController(ILogger<TestController> logger, AppDbContext ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        // GET
        public async  Task<IActionResult> Test()
        {
            _logger.LogInformation("Test method starts");
            var vm = new TestViewModel
            {
                PaymentTypes = await _ctx
                    .PaymentTypes
                    .Include(x => x.PaymentTypeName)
                    .ThenInclude(x => x!.Translations)
                    .ToListAsync()
            };
            _logger.LogInformation("Test method pre-return");
            return View(vm);
        }

        [Authorize]
        public  string TestAuth()
        {
            return "OK";
        }
    }
}