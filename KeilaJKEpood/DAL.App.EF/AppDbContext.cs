using System;
using Domain.App;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext
    {
        
        public DbSet<Person> Persons { get; set; } = default!;
        public DbSet<Bill> Bills { get; set; } = default!;
        public DbSet<Company> Companies { get; set; } = default!;
        public DbSet<Discount> Discounts { get; set; } = default!;
        public DbSet<LineOnBill> LinesOnBills { get; set; } = default!;
        public DbSet<Order> Orders { get; set; } = default!;
        public DbSet<Payment> Payments { get; set; } = default!;
        public DbSet<PaymentType> PaymentTypes { get; set; } = default!;
        public DbSet<Price> Prices { get; set; } = default!;
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<ProductInOrder> ProductsInOrders { get; set; } = default!;
        public DbSet<ProductType> ProductTypes { get; set; } = default!;
        public DbSet<Warehouse> Warehouses { get; set; } = default!;
        
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
           
        }

    }
}