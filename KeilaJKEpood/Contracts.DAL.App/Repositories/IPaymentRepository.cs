﻿using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;

namespace Contracts.DAL.App.Repositories
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        // add your Payment custom method declarations here
        Task DeleteAllByNameAsync(string name);
    }
}