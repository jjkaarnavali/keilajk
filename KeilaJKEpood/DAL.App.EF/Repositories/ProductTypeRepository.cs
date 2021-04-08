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
using ProductTypeMapper = DAL.App.EF.Mappers.ProductTypeMapper;

namespace DAL.App.EF.Repositories
{
    public class ProductTypeRepository : BaseRepository<DAL.App.DTO.ProductType, Domain.App.ProductType, AppDbContext>, IProductTypeRepository
    {
        public ProductTypeRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ProductTypeMapper(mapper))
        {
        }
        
        
        public override async Task<IEnumerable<DAL.App.DTO.ProductType>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            /*query = query
                .Include(p => p.TypeName);*/
            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();

            
            
            return res!;
        }
        
        public override async Task<DAL.App.DTO.ProductType?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
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