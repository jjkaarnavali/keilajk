using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IOrderRepository: IBaseRepository<DALAppDTO.Order>, IOrderRepositoryCustom<DALAppDTO.Order>
    {
       
    }

    public interface IOrderRepositoryCustom<TEntity>
    {
    }

}