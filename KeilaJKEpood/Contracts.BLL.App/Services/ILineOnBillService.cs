using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface ILineOnBillService: IBaseEntityService<BLLAppDTO.LineOnBill, DALAppDTO.LineOnBill>, IDiscountRepositoryCustom<BLLAppDTO.LineOnBill>
    {
        //Task<IEnumerable<BLLAppDTO.LineOnBill>> GetAllLinesOnBillsWithInfo(Guid userId);
    }
}