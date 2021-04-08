using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ILineOnBillRepository: IBaseRepository<DALAppDTO.LineOnBill>, ILineOnBillRepositoryCustom<DALAppDTO.LineOnBill>
    {
       
    }

    public interface ILineOnBillRepositoryCustom<TEntity>
    {
    }

}