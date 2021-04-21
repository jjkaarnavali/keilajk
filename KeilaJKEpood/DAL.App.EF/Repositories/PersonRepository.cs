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
        
        public override async Task<IEnumerable<DAL.App.DTO.Person>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);

            /*if (noTracking)
            {
                query = query.AsNoTracking();
            }*/
            if (userId != default)
            {
                query = query.Where(c => c.AppUserId == userId);
            }

            
            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();

            
            
            return res!;
        }
        
        public override async Task<DAL.App.DTO.Person?> FirstOrDefaultAsync(Guid id, Guid userId = default, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
            

            var res = await query.FirstOrDefaultAsync(m => m.Id == id && m.AppUserId == userId);

            return Mapper.Map(res);

        }

        public async Task<IEnumerable<DAL.App.DTO.Person>> GetAllNewAsync(Guid userId, bool noTracking = true)
        {
            var query = CreateQuery(userId, noTracking);
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