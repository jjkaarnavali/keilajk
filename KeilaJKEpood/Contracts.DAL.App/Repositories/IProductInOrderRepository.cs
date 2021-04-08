using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IProductInOrderRepository: IBaseRepository<DALAppDTO.ProductInOrder>, IProductInOrderRepositoryCustom<DALAppDTO.ProductInOrder>
    {
       
    }

    public interface IProductInOrderRepositoryCustom<TEntity>
    {
    }

}