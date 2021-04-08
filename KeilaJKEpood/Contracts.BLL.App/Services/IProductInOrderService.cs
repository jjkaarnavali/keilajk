using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App.Repositories;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.BLL.App.Services
{
    public interface IProductInOrderService: IBaseEntityService<BLLAppDTO.ProductInOrder, DALAppDTO.ProductInOrder>, IProductInOrderRepositoryCustom<BLLAppDTO.ProductInOrder>
    {
        //Task<IEnumerable<BLLAppDTO.ProductInOrder>> GetAllProductsInOrdersWithInfo(Guid userId);
    }
}