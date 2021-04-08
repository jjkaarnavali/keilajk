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
using WarehouseMapper = DAL.App.EF.Mappers.WarehouseMapper;

namespace DAL.App.EF.Repositories
{
    public class WarehouseRepository : BaseRepository<DAL.App.DTO.Warehouse, Domain.App.Warehouse, AppDbContext>, IWarehouseRepository
    {
        public WarehouseRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new WarehouseMapper(mapper))
        {
        }
        
        
        public override async Task<IEnumerable<DAL.App.DTO.Warehouse>> GetAllAsync(Guid userId, bool noTracking = true)
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
            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();

            return res!;
        }
        
        public override async Task<DAL.App.DTO.Warehouse?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
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