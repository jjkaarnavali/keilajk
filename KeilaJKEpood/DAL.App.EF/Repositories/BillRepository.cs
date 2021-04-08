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
using BillMapper = DAL.App.EF.Mappers.BillMapper;

namespace DAL.App.EF.Repositories
{
    public class BillRepository : BaseRepository<DAL.App.DTO.Bill, Domain.App.Bill, AppDbContext>, IBillRepository
    {
        public BillRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new BillMapper(mapper))
        {
        }
        
        
        public override async Task<IEnumerable<DAL.App.DTO.Bill>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            /*query = query
                .Include(p => p.BillNr)
                .Include(p => p.CreationTime)
                .Include(p => p.OrderId)
                .Include(p => p.PersonId)
                .Include(p => p.UserId)
                .Include(p => p.PriceToPay)
                .Include(p => p.PriceWithoutTax)
                .Include(p => p.SumOfTax);*/

            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();

            
            
            return res!;
        }

        public override async Task<DAL.App.DTO.Bill?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
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