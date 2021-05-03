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
using Company = DAL.App.DTO.Company;
using CompanyMapper = DAL.App.EF.Mappers.CompanyMapper;


namespace DAL.App.EF.Repositories
{
    public class CompanyRepository : BaseRepository<DAL.App.DTO.Company, Domain.App.Company, AppDbContext>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CompanyMapper(mapper))
        {
        }
        
        public override Company Update(Company entity)
        {
            var domainEntity = Mapper.Map(entity);

            // load the translations (will lose the dal mapper translations)
            domainEntity!.CompanyName = 
                RepoDbContext.LangStrings
                    .Include(t => t.Translations)
                    .First(x => x.Id == domainEntity.CompanyNameId);
            // set the value from dal entity back to list
            domainEntity!.CompanyName.SetTranslation(entity.CompanyName);
            
            domainEntity!.RegistrationCode = 
                RepoDbContext.LangStrings
                    .Include(t => t.Translations)
                    .First(x => x.Id == domainEntity.RegistrationCodeId);
            // set the value from dal entity back to list
            domainEntity!.RegistrationCode.SetTranslation(entity.RegistrationCode);
            
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
            
            var updatedEntity = RepoDbSet.Update(domainEntity!).Entity;
            var dalEntity = Mapper.Map(updatedEntity);
            return dalEntity!;
        }
        
        public override async Task<IEnumerable<DAL.App.DTO.Company>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }
            
            query = query
                .Include(p => p.CompanyName)
                .ThenInclude(t => t!.Translations);
            
            query = query
                .Include(p => p.RegistrationCode)
                .ThenInclude(t => t!.Translations);
            
            query = query
                .Include(p => p.Phone)
                .ThenInclude(t => t!.Translations);
            
            query = query
                .Include(p => p.Email)
                .ThenInclude(t => t!.Translations);

            /*query = query
                .Include(p => p.Email)
                .Include(p => p.Phone)
                .Include(p => p.CompanyName)
                .Include(p => p.RegistrationCode);*/
            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();

            
            
            return res!;
        }
        
        public override async Task<DAL.App.DTO.Company?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();
            
            query = query
                .Include(p => p.CompanyName)
                .ThenInclude(t => t!.Translations);
            
            query = query
                .Include(p => p.RegistrationCode)
                .ThenInclude(t => t!.Translations);
            
            query = query
                .Include(p => p.Phone)
                .ThenInclude(t => t!.Translations);
            
            query = query
                .Include(p => p.Email)
                .ThenInclude(t => t!.Translations);

            if (noTracking)
            {
                query = query.AsNoTracking();
            }
            
            
            
            var res = await query.FirstOrDefaultAsync(m => m.Id == id);

            return Mapper.Map(res);
        }
    }
}