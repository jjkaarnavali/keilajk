using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IProductInWarehouseRepository: IBaseRepository<DALAppDTO.ProductInWarehouse>, IProductInWarehouseRepositoryCustom<DALAppDTO.ProductInWarehouse>
    {
       
    }

    public interface IProductInWarehouseRepositoryCustom<TEntity>
    {
    }

}