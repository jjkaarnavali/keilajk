using System.Collections.Generic;
using Domain.App;
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
            var p = new Person() {FirstName = "Foo", LastName = "Lmao", PersonsIdCode = "11111111111"};
            
            ctx.SaveChanges();
        }
        
    }
}