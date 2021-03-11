using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class BillRepository : BaseRepository<Bill>, IBillRepository
    {
        public BillRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public Task DeleteAllByNameAsync(string name)
        {
            throw new System.NotImplementedException();
        }
        
        public override async Task<IEnumerable<Bill>> GetAllAsync(bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            query = query
                .Include(p => p.BillNr)
                .Include(p => p.CreationTime)
                .Include(p => p.OrderId)
                .Include(p => p.PersonId)
                .Include(p => p.UserId)
                .Include(p => p.PriceToPay)
                .Include(p => p.PriceWithoutTax)
                .Include(p => p.SumOfTax);
            var res = await query.ToListAsync();

            
            
            return res;
        }
    }
}