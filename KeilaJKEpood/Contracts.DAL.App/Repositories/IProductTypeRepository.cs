using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;

namespace Contracts.DAL.App.Repositories
{
    public interface IProductTypeRepository : IBaseRepository<ProductType>
    {
        // add your ProductType custom method declarations here
        Task DeleteAllByNameAsync(string name);
    }
}