using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;

namespace Contracts.DAL.App.Repositories
{
    public interface IWarehouseRepository : IBaseRepository<Warehouse>
    {
        // add your Warehouse custom method declarations here
        Task DeleteAllByNameAsync(string name);
    }
}