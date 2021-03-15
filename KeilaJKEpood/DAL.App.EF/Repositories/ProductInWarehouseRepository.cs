using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ProductInWarehouseRepository : BaseRepository<ProductInWarehouse, AppDbContext>, IProductInWarehouseRepository
    {
        public ProductInWarehouseRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public Task DeleteAllByNameAsync(string name)
        {
            throw new System.NotImplementedException();
        }
        
        public override async Task<IEnumerable<ProductInWarehouse>> GetAllAsync(bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            /*query = query
                .Include(p => p.WarehouseId)
                .Include(p => p.ProductAmount)
                .Include(p => p.ProductId);*/
            var res = await query.ToListAsync();

            
            
            return res;
        }
    }
}