using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IProductRepository: IBaseRepository<DALAppDTO.Product>, IProductRepositoryCustom<DALAppDTO.Product>
    {
       
    }

    public interface IProductRepositoryCustom<TEntity>
    {
    }

}