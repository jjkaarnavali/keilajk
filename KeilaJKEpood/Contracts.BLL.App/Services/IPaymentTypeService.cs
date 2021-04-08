using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IPaymentTypeService: IBaseEntityService<BLLAppDTO.PaymentType, DALAppDTO.PaymentType>, IPaymentTypeRepositoryCustom<BLLAppDTO.PaymentType>
    {
        //Task<IEnumerable<BLLAppDTO.PaymentType>> GetAllPaymentTypesWithInfo(Guid userId);
    }
}