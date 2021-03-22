using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Contracts.DAL.Base;
using DAL.Base;
using System;
using System.Collections.Generic;


namespace DAL.Base.EF
{
    public class BaseUnitOfWork<TDbContext> : DAL.Base.BaseUnitOfWork
        where TDbContext: DbContext
    {
        protected readonly TDbContext UowDbContext;
        
        public BaseUnitOfWork(TDbContext uowDbContext)
        {
            UowDbContext = uowDbContext;
        }
        
        public override Task<int> SaveChangesAsync()
        {
            return UowDbContext.SaveChangesAsync();
        }
    }
}