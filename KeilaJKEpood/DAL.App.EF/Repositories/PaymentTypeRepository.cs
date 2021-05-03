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
using PaymentType = DAL.App.DTO.PaymentType;
using PaymentTypeMapper = DAL.App.EF.Mappers.PaymentTypeMapper;

namespace DAL.App.EF.Repositories
{
    public class PaymentTypeRepository : BaseRepository<DAL.App.DTO.PaymentType, Domain.App.PaymentType, AppDbContext>, IPaymentTypeRepository
    {
        public PaymentTypeRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new PaymentTypeMapper(mapper))
        {
        }
        
        public override PaymentType Update(PaymentType entity)
        {
            var domainEntity = Mapper.Map(entity);

            // load the translations (will lose the dal mapper translations)
            domainEntity!.PaymentTypeName = 
                RepoDbContext.LangStrings
                    .Include(t => t.Translations)
                    .First(x => x.Id == domainEntity.PaymentTypeNameId);
            // set the value from dal entity back to list
            domainEntity!.PaymentTypeName.SetTranslation(entity.PaymentTypeName);
            
            var updatedEntity = RepoDbSet.Update(domainEntity!).Entity;
            var dalEntity = Mapper.Map(updatedEntity);
            return dalEntity!;
        }



        public Task DeleteAllByNameAsync(string name)
        {
            throw new System.NotImplementedException();
        }
        
        public override async Task<IEnumerable<DAL.App.DTO.PaymentType>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            query = query
                .Include(p => p.PaymentTypeName)
                .ThenInclude(t => t!.Translations);
            
            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();

            
            return res!;
        }
        
        public override async Task<DAL.App.DTO.PaymentType?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            query = query
                .Include(c => c.PaymentTypeName)
                .ThenInclude(t => t!.Translations);
            
            var res = await query.FirstOrDefaultAsync(m => m.Id == id);

            return Mapper.Map(res);
        }
        

    }
}