using System;
using System.Linq;
using Domain.App;
using Domain.App.Identity;
using Domain.Base;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
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
        public DbSet<ProductInWarehouse> ProductsInWarehouses { get; set; } = default!;
        public DbSet<ProductType> ProductTypes { get; set; } = default!;
        public DbSet<Warehouse> Warehouses { get; set; } = default!;
        
        public DbSet<LangString> LangStrings { get; set; } = default!;
        public DbSet<Translation> Translations { get; set; } = default!;

        
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
           
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // TODO: should be in base dbcontext
            builder.Entity<Translation>().HasKey(k => new { k.Culture, k.LangStringId});
            
            // disable cascade delete initially for everything
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            /*
            builder.Entity<Contact>()
                .HasIndex(x => new {x.PersonId, x.ContactTypeId})
                .IsUnique();


            builder.Entity<ContactType>()
                .HasMany(m => m.Contacts)
                .WithOne(o => o.ContactType!)
                .OnDelete(DeleteBehavior.Cascade);

            
            // You cannot have two delete paths in parallel!
            builder.Entity<ContactType>()
                .HasMany(m => m.SecondaryContacts)
                .WithOne(o => o.SecondaryContactType!)
                .OnDelete(DeleteBehavior.Cascade);
            */
        }


    }
}