using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class WarehouseRepository : BaseRepository<Warehouse, AppDbContext>, IWarehouseRepository
    {
        public WarehouseRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public Task DeleteAllByNameAsync(string name)
        {
            throw new System.NotImplementedException();
        }
        
        public override async Task<IEnumerable<Warehouse>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            /*query = query
                .Include(p => p.Address)
                .Include(p => p.Email)
                .Include(p => p.Phone)
                .Include(p => p.WarehouseCode);*/
            var res = await query.ToListAsync();

            
            
            return res;
        }
    }
}