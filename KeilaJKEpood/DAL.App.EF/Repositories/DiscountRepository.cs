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
using Discount = DAL.App.DTO.Discount;
using DiscountMapper = DAL.App.EF.Mappers.DiscountMapper;

namespace DAL.App.EF.Repositories
{
    public class DiscountRepository : BaseRepository<DAL.App.DTO.Discount, Domain.App.Discount, AppDbContext>, IDiscountRepository
    {
        public DiscountRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new DiscountMapper(mapper))
        {
        }

        public override Discount Update(Discount entity)
        {
            var domainEntity = Mapper.Map(entity);

            // load the translations (will lose the dal mapper translations)
            domainEntity!.DiscountName = 
                RepoDbContext.LangStrings
                    .Include(t => t.Translations)
                    .First(x => x.Id == domainEntity.DiscountNameId);
            // set the value from dal entity back to list
            domainEntity!.DiscountName.SetTranslation(entity.DiscountName);
            
            domainEntity!.RequiredUserLevel = 
                RepoDbContext.LangStrings
                    .Include(t => t.Translations)
                    .First(x => x.Id == domainEntity.RequiredUserLevelId);
            // set the value from dal entity back to list
            domainEntity!.RequiredUserLevel.SetTranslation(entity.RequiredUserLevel!);
            
            domainEntity!.DiscountPercentage = 
                RepoDbContext.LangStrings
                    .Include(t => t.Translations)
                    .First(x => x.Id == domainEntity.DiscountPercentageId);
            // set the value from dal entity back to list
            domainEntity!.DiscountPercentage.SetTranslation(entity.DiscountPercentage);
            
            var updatedEntity = RepoDbSet.Update(domainEntity!).Entity;
            var dalEntity = Mapper.Map(updatedEntity);
            return dalEntity!;
        }
        
        
        public override async Task<IEnumerable<DAL.App.DTO.Discount>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }
            
            query = query
                .Include(p => p.RequiredUserLevel)
                .ThenInclude(t => t!.Translations);
            
            query = query
                .Include(p => p.DiscountPercentage)
                .ThenInclude(t => t!.Translations);
            
            query = query
                .Include(p => p.DiscountName)
                .ThenInclude(t => t!.Translations);

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
            
            query = query
                .Include(p => p.RequiredUserLevel)
                .ThenInclude(t => t!.Translations);
            
            query = query
                .Include(p => p.DiscountPercentage)
                .ThenInclude(t => t!.Translations);
            
            query = query
                .Include(p => p.DiscountName)
                .ThenInclude(t => t!.Translations);
            
            
            
            var res = await query.FirstOrDefaultAsync(m => m.Id == id);

            return Mapper.Map(res);
        }
    }
}