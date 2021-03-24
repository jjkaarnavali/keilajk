using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ProductRepository : BaseRepository<Product, AppDbContext>, IProductRepository
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public Task DeleteAllByNameAsync(string name)
        {
            throw new System.NotImplementedException();
        }
        
        public override async Task<IEnumerable<Product>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            /*query = query
                .Include(p => p.CompanyId)
                .Include(p => p.ProductCode)
                .Include(p => p.ProductName)
                .Include(p => p.ProductSeason)
                .Include(p => p.ProductSize)
                .Include(p => p.ProductTypeId);*/
            var res = await query.ToListAsync();

            
            
            return res;
        }
    }
}