using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IWarehouseRepository: IBaseRepository<DALAppDTO.Warehouse>, IWarehouseRepositoryCustom<DALAppDTO.Warehouse>
    {
       
    }

    public interface IWarehouseRepositoryCustom<TEntity>
    {
    }

}