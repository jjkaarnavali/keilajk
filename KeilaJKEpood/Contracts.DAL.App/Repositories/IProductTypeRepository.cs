using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IProductTypeRepository: IBaseRepository<DALAppDTO.ProductType>, IProductTypeRepositoryCustom<DALAppDTO.ProductType>
    {
       
    }

    public interface IProductTypeRepositoryCustom<TEntity>
    {
    }

}