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
using ProductMapper = DAL.App.EF.Mappers.ProductMapper;

namespace DAL.App.EF.Repositories
{
    public class ProductRepository : BaseRepository<DAL.App.DTO.Product, Domain.App.Product, AppDbContext>, IProductRepository
    {
        public ProductRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new ProductMapper(mapper))
        {
        }
        
        
        public override async Task<IEnumerable<DAL.App.DTO.Product>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            /*query = query
                .Include(p => p.CompanyId)
                .Include(p => p.ProductCode)
                .Include(p => p.ProductName)
                .Include(p => p.ProductSeason)
                .Include(p => p.ProductSize)
                .Include(p => p.ProductTypeId);*/
            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();
            
            
            return res!;
        }
        
        public override async Task<DAL.App.DTO.Product?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
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