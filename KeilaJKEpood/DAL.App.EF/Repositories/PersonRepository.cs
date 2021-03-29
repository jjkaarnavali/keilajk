using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class PersonRepository : BaseRepository<Person, AppDbContext>, IPersonRepository
    {
        public PersonRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public async Task DeleteAllByNameAsync(string name, Guid userId)
        {
            foreach (var person in await RepoDbSet.Where(x => x.FirstName == name).ToListAsync())
            {
                Remove(person, userId);
            }
        }
        
        public override async Task<IEnumerable<Person>> GetAllAsync(Guid userId, bool noTracking = true)
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

            
            var res = await query.ToListAsync();

            
            
            return res;
        }
        
        public override async Task<Person?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }
            
            
            
            var res = await query.FirstOrDefaultAsync(m => m.Id == id && m.AppUserId == userId);

            return res;
        }


    }
}