using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;

namespace Contracts.BLL.App.Services
{
    public interface IPurchaseReceivedPageService: IBaseService
    {
        Task CreatePayment(Guid id, Guid userId);
    }
}