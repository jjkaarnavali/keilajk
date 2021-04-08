using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;
using DALAppDTO = DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface ICompanyRepository: IBaseRepository<DALAppDTO.Company>, ICompanyRepositoryCustom<DALAppDTO.Company>
    {
       
    }

    public interface ICompanyRepositoryCustom<TEntity>
    {
    }

}