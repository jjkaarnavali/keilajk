using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;

namespace Contracts.DAL.App.Repositories
{
    public interface IProductInWarehouseRepository : IBaseRepository<ProductInWarehouse>
    {
        // add your ProductInWarehouse custom method declarations here
        Task DeleteAllByNameAsync(string name);
    }
}