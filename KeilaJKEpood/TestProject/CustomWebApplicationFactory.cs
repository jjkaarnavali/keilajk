using System;
using System.Linq;
using DAL.App.EF;
using Domain.App;
using Domain.Base;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup>
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // find the dbcontext
                var descriptor = services
                    .SingleOrDefault(d =>
                        d.ServiceType == typeof(DbContextOptions<AppDbContext>)
                    );
                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                services.AddDbContext<AppDbContext>(options =>
                {
                    // do we need unique db?
                    options.UseInMemoryDatabase(builder.GetSetting("test_database_name"));
                });

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<AppDbContext>();

                db.Database.EnsureCreated();
                
                // data is already seeded
                if (db.PaymentTypes.Any()) return;
                
                // seed data

                
                db.PaymentTypes.Add(new PaymentType()
                {
                    PaymentTypeName = new LangString("Type 0")
                });
                var nikeId = Guid.NewGuid();
                db.Companies.Add(new Company()
                {
                    Id = nikeId,
                    CompanyName = new LangString("nike"),
                    From = DateTime.Now
                });
                var productTypeId = Guid.NewGuid();
                db.ProductTypes.Add(new ProductType()
                {
                    Id = productTypeId,
                    TypeName = "home shirt"
                });
                var productId = Guid.NewGuid();
                db.Products.Add(new Product()
                {
                    Id = productId,
                    CompanyId = nikeId,
                    ProductTypeId = productTypeId,
                    ProductName = "home shirt",
                    ProductCode = "1",
                    ProductSeason = "1999/2000",
                    ProductSize = "S",
                    From = DateTime.Now
                });
                var discountId = Guid.NewGuid();
                db.Discounts.Add(new Discount()
                {
                    Id = discountId,
                    RequiredUserLevel = "0",
                    DiscountPercentage = "0",
                    DiscountName = "soodus",
                    From = DateTime.Now
                });
                var priceId = Guid.NewGuid();
                db.Prices.Add(new Price()
                {
                    Id = priceId,
                    ProductId = productId,
                    DiscountId = discountId,
                    PriceInEur = 5,
                    From = DateTime.Now
                });
                db.SaveChanges();
                var products = db.Products;
            });
        }
    }
}