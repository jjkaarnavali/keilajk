using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPaymentRepository: IBaseRepository<DALAppDTO.Payment>, IPaymentRepositoryCustom<DALAppDTO.Payment>
    {
       
    }

    public interface IPaymentRepositoryCustom<TEntity>
    {
    }

}