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
using PaymentMapper = DAL.App.EF.Mappers.PaymentMapper;


namespace DAL.App.EF.Repositories
{
    public class PaymentRepository : BaseRepository<DAL.App.DTO.Payment, Domain.App.Payment, AppDbContext>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext dbContext, IMapper mapper) : base(dbContext, new PaymentMapper(mapper))
        {
        }

        public override async Task<IEnumerable<DAL.App.DTO.Payment>> GetAllAsync(Guid userId, bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            /*query = query
                .Include(p => p.BillId)
                .Include(p => p.PaymentTime)
                .Include(p => p.PersonId)
                .Include(p => p.PaymentTypeId);*/
            var res = await query.Select(x => Mapper.Map(x)).ToListAsync();

            
            
            return res!;
        }
        
        public override async Task<DAL.App.DTO.Payment?> FirstOrDefaultAsync(Guid id, Guid userId, bool noTracking = true)
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