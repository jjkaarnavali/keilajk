using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.App.Mappers;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using DTO.App;
using Microsoft.EntityFrameworkCore;
using Domain.App;
using LineOnBillMapper = DAL.App.EF.Mappers.LineOnBillMapper;

namespace DAL.App.EF.Repositories
{
    public class LineOnBillRepository : BaseRepository<DAL.App.DTO.LineOnBill, Domain.App.LineOnBill, AppDbContext>, ILineOnBillRepository
    {
        public LineOnBillRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new LineOnBillMapper(mapper))
        {
        }
        
        
        public override async Task<IEnumerable<DAL.App.DTO.LineOnBill>> GetAllAsync(Guid userId, bool noTracking = true)
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
            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();

            
            
            return res!;
        }
        
        public override async Task<DAL.App.DTO.LineOnBill?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }
            
            
            
            var res = await query.FirstOrDefaultAsync(m => m.Id == id);

            return Mapper.Map(res);
        }
    }
}