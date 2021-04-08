using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IPaymentTypeRepository: IBaseRepository<DALAppDTO.PaymentType>, IPaymentTypeRepositoryCustom<DALAppDTO.PaymentType>
    {
       
    }

    public interface IPaymentTypeRepositoryCustom<TEntity>
    {
    }

}