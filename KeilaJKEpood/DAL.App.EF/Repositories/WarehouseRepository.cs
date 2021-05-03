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
using Warehouse = DAL.App.DTO.Warehouse;
using WarehouseMapper = DAL.App.EF.Mappers.WarehouseMapper;

namespace DAL.App.EF.Repositories
{
    public class WarehouseRepository : BaseRepository<DAL.App.DTO.Warehouse, Domain.App.Warehouse, AppDbContext>, IWarehouseRepository
    {
        public WarehouseRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new WarehouseMapper(mapper))
        {
        }
        
        public override Warehouse Update(Warehouse entity)
        {
            var domainEntity = Mapper.Map(entity);

            // load the translations (will lose the dal mapper translations)
            domainEntity!.Address = 
                RepoDbContext.LangStrings
                    .Include(t => t.Translations)
                    .First(x => x.Id == domainEntity.AddressId);
            // set the value from dal entity back to list
            domainEntity!.Address.SetTranslation(entity.Address);
            
            domainEntity!.Phone = 
                RepoDbContext.LangStrings
                    .Include(t => t.Translations)
                    .First(x => x.Id == domainEntity.PhoneId);
            // set the value from dal entity back to list
            domainEntity!.Phone.SetTranslation(entity.Phone);

            domainEntity!.Email = 
                RepoDbContext.LangStrings
                    .Include(t => t.Translations)
                    .First(x => x.Id == domainEntity.EmailId);
            // set the value from dal entity back to list
            domainEntity!.Email.SetTranslation(entity.Email);
            
            domainEntity!.WarehouseCode = 
                RepoDbContext.LangStrings
                    .Include(t => t.Translations)
                    .First(x => x.Id == domainEntity.WarehouseCodeId);
            // set the value from dal entity back to list
            domainEntity!.WarehouseCode.SetTranslation(entity.WarehouseCode);
            
            var updatedEntity = RepoDbSet.Update(domainEntity!).Entity;
            var dalEntity = Mapper.Map(updatedEntity);
            return dalEntity!;
        }
        
        public override async Task<IEnumerable<DAL.App.DTO.Warehouse>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }
            
            query = query
                .Include(p => p.Address)
                .ThenInclude(t => t!.Translations);
            
            query = query
                .Include(p => p.WarehouseCode)
                .ThenInclude(t => t!.Translations);
            
            query = query
                .Include(p => p.Phone)
                .ThenInclude(t => t!.Translations);
            
            query = query
                .Include(p => p.Email)
                .ThenInclude(t => t!.Translations);

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
            
            query = query
                .Include(p => p.Address)
                .ThenInclude(t => t!.Translations);
            
            query = query
                .Include(p => p.WarehouseCode)
                .ThenInclude(t => t!.Translations);
            
            query = query
                .Include(p => p.Phone)
                .ThenInclude(t => t!.Translations);
            
            query = query
                .Include(p => p.Email)
                .ThenInclude(t => t!.Translations);
            
            
            var res = await query.FirstOrDefaultAsync(m => m.Id == id);

            return Mapper.Map(res);
        }
    }
}