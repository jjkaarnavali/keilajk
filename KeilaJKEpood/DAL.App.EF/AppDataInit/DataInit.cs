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
            
            
            ctx.SaveChanges();
        }
        
    }
}