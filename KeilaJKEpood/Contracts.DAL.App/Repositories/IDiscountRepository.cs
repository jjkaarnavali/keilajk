using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IDiscountRepository: IBaseRepository<DALAppDTO.Discount>, IDiscountRepositoryCustom<DALAppDTO.Discount>
    {
       
    }

    public interface IDiscountRepositoryCustom<TEntity>
    {
    }

}