using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IBillRepository: IBaseRepository<DALAppDTO.Bill>, IBillRepositoryCustom<DALAppDTO.Bill>
    {
       
    }

    public interface IBillRepositoryCustom<TEntity>
    {
    }

}