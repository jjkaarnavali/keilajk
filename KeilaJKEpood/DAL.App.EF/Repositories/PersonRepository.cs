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
using PersonMapper = DAL.App.EF.Mappers.PersonMapper;


namespace DAL.App.EF.Repositories
{
    
    public class PersonRepository : BaseRepository<DAL.App.DTO.Person, Domain.App.Person, AppDbContext>, IPersonRepository
    {

        public PersonRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new PersonMapper(mapper))
        {
        }


        /*public async Task DeleteAllByNameAsync(string name, Guid userId)
        {
            foreach (var person in await RepoDbSet.Where(x => x.FirstName == name).ToListAsync())
            {
                Remove(person, userId);
            }
        }*/
        
        public override Person Update(Person entity)
        {
            var domainEntity = Mapper.Map(entity);

            // load the translations (will lose the dal mapper translations)
            domainEntity!.FirstName = 
                RepoDbContext.LangStrings
                    .Include(t => t.Translations)
                    .First(x => x.Id == domainEntity.FirstNameId);
            // set the value from dal entity back to list
            domainEntity!.FirstName.SetTranslation(entity.FirstName);
            
            domainEntity!.LastName = 
                RepoDbContext.LangStrings
                    .Include(t => t.Translations)
                    .First(x => x.Id == domainEntity.LastNameId);
            // set the value from dal entity back to list
            domainEntity!.LastName.SetTranslation(entity.LastName);
            
            domainEntity!.PersonsIdCode = 
                RepoDbContext.LangStrings
                    .Include(t => t.Translations)
                    .First(x => x.Id == domainEntity.PersonsIdCodeId);
            // set the value from dal entity back to list
            domainEntity!.PersonsIdCode.SetTranslation(entity.PersonsIdCode);
            
            var updatedEntity = RepoDbSet.Update(domainEntity!).Entity;
            var dalEntity = Mapper.Map(updatedEntity);
            return dalEntity!;
        }
        
        
        public override async Task<IEnumerable<DAL.App.DTO.Person>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }
            /*if (userId != default)
            {
                query = query.Where(c => c.AppUserId == userId);
            }*/
            query = query
                .Include(p => p.FirstName)
                .ThenInclude(t => t!.Translations);
            
            query = query
                .Include(p => p.LastName)
                .ThenInclude(t => t!.Translations);
            
            query = query
                .Include(p => p.PersonsIdCode)
                .ThenInclude(t => t!.Translations);

            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();
            
            return res!;
        }
        
        public override async Task<DAL.App.DTO.Person?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(p => p.FirstName)
                .ThenInclude(t => t!.Translations);
            
            query = query
                .Include(p => p.LastName)
                .ThenInclude(t => t!.Translations);
            
            query = query
                .Include(p => p.PersonsIdCode)
                .ThenInclude(t => t!.Translations);
            

            var res = await query.FirstOrDefaultAsync(m => m.Id == id && m.AppUserId == userId);

            
            return Mapper.Map(res);

        }

        public async Task<IEnumerable<DAL.App.DTO.Person>> GetAllNewAsync(Guid userId, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            query = query
                .Include(p => p.FirstName)
                .ThenInclude(t => t!.Translations);
            
            query = query
                .Include(p => p.LastName)
                .ThenInclude(t => t!.Translations);
            
            query = query
                .Include(p => p.PersonsIdCode)
                .ThenInclude(t => t!.Translations);
            var resQuery = query.Select(p => new DAL.App.DTO.Person()
                {
                    Id = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    PersonsIdCode = p.PersonsIdCode
                })
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName);

            return await resQuery.ToListAsync();

        }
    }
}