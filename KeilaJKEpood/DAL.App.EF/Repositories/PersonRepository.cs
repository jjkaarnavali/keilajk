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

        public async Task DeleteAllByNameAsync(string name)
        {
            foreach (var person in await RepoDbSet.Where(x => x.FirstName == name).ToListAsync())
            {
                Remove(person);
            }
        }
        
        public override async Task<IEnumerable<Person>> GetAllAsync(bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            /*query = query
                .Include(p => p.FirstName)
                .Include(p => p.LastName)
                .Include(p => p.PersonsIdCode);*/
            var res = await query.ToListAsync();

            
            
            return res;
        }

    }
}