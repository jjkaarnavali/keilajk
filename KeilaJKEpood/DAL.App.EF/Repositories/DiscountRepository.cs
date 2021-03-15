using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class DiscountRepository : BaseRepository<Discount, AppDbContext>, IDiscountRepository
    {
        public DiscountRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public Task DeleteAllByNameAsync(string name)
        {
            throw new System.NotImplementedException();
        }
        
        public override async Task<IEnumerable<Discount>> GetAllAsync(bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            /*query = query
                .Include(p => p.DiscountName)
                .Include(p => p.DiscountPercentage)
                .Include(p => p.RequiredUserLevel);*/
            var res = await query.ToListAsync();

            
            
            return res;
        }
    }
}