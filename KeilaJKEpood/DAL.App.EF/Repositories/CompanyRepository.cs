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
using CompanyMapper = DAL.App.EF.Mappers.CompanyMapper;


namespace DAL.App.EF.Repositories
{
    public class CompanyRepository : BaseRepository<DAL.App.DTO.Company, Domain.App.Company, AppDbContext>, ICompanyRepository
    {
        public CompanyRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new CompanyMapper(mapper))
        {
        }
        
        
        public override async Task<IEnumerable<DAL.App.DTO.Company>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

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

            if (noTracking)
            {
                query = query.AsNoTracking();
            }
            
            
            
            var res = await query.FirstOrDefaultAsync(m => m.Id == id);

            return Mapper.Map(res);
        }
    }
}