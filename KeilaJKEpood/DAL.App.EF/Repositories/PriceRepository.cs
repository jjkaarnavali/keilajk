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
using PriceMapper = DAL.App.EF.Mappers.PriceMapper;

namespace DAL.App.EF.Repositories
{
    public class PriceRepository : BaseRepository<DAL.App.DTO.Price, Domain.App.Price, AppDbContext>, IPriceRepository
    {
        public PriceRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new PriceMapper(mapper))
        {
        }
        
        
        public override async Task<IEnumerable<DAL.App.DTO.Price>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            /*query = query
                .Include(p => p.DiscountId)
                .Include(p => p.ProductId)
                .Include(p => p.PriceInEur);*/
            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();

            
            
            return res!;
        }
        
        public override async Task<DAL.App.DTO.Price?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
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