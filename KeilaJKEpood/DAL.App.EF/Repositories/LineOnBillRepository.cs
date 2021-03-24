using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class LineOnBillRepository : BaseRepository<LineOnBill, AppDbContext>, ILineOnBillRepository
    {
        public LineOnBillRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public Task DeleteAllByNameAsync(string name)
        {
            throw new System.NotImplementedException();
        }
        
        public override async Task<IEnumerable<LineOnBill>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            /*query = query
                .Include(p => p.Amount)
                .Include(p => p.BillId)
                .Include(p => p.PriceId)
                .Include(p => p.ProductId)
                .Include(p => p.TaxPercentage)
                .Include(p => p.PriceToPay)
                .Include(p => p.PriceWithoutTax)
                .Include(p => p.SumOfTax);*/
            var res = await query.ToListAsync();

            
            
            return res;
        }
    }
}