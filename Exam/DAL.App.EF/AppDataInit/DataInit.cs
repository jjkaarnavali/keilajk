using System;
using System.Collections.Generic;
using Domain.App;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.AppDataInit
{
    public static class DataInit
    {

        public static void DropDatabase(AppDbContext ctx)
        {
            ctx.Database.EnsureDeleted();
        }

        public static void MigrateDatabase(AppDbContext ctx)
        {
            ctx.Database.Migrate();
        }
        
        public static void SeedAppData(AppDbContext ctx)
        {
            var poll = new Category() {Id = Guid.NewGuid(), CategoryName = "Poll"};
            var quiz = new Category() {Id = Guid.NewGuid(), CategoryName = "Quiz"};

            
            ctx.Categories.Add(poll);
            ctx.Categories.Add(quiz);
            
            ctx.SaveChanges();
        }

        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var role = new AppRole();
            role.Name = "Admin";
            var result = roleManager.CreateAsync(role).Result;
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    Console.WriteLine("Cant create role! Error: " + identityError.Description);
                }
            }

            var user = new AppUser();
            user.Email = "admin@gmail.com";
            user.FirstName = "Admin";
            user.LastName = "admin.com";
            user.UserName = user.Email;

            result = userManager.CreateAsync(user, "Telopoiss4.").Result;
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    Console.WriteLine("Cant create user! Error: " + identityError.Description);
                }
            }

            result = userManager.AddToRoleAsync(user, "Admin").Result;
            
            
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    Console.WriteLine("Cant add user to role! Error: " + identityError.Description);
                }
            }
/*
            result = userManager.UpdateAsync(user).Result;
            if (!result.Succeeded)
            {
                foreach (var identityError in result.Errors)
                {
                    Console.WriteLine("Cant update user! Error: " + identityError.Description);
                }
            }
*/            
        }

    }
}