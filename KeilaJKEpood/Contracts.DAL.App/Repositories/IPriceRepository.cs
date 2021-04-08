using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPriceRepository: IBaseRepository<DALAppDTO.Price>, IPriceRepositoryCustom<DALAppDTO.Price>
    {
       
    }

    public interface IPriceRepositoryCustom<TEntity>
    {
    }

}