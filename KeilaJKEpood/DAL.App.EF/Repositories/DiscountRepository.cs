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
using DiscountMapper = DAL.App.EF.Mappers.DiscountMapper;

namespace DAL.App.EF.Repositories
{
    public class DiscountRepository : BaseRepository<DAL.App.DTO.Discount, Domain.App.Discount, AppDbContext>, IDiscountRepository
    {
        public DiscountRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DiscountMapper(mapper))
        {
        }

        
        
        public override async Task<IEnumerable<DAL.App.DTO.Discount>> GetAllAsync(Guid userId, bool noTracking = true)
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
            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();

            
            
            return res!;
        }
        public override async Task<DAL.App.DTO.Discount?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
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