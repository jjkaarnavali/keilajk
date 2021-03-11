using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;

namespace Contracts.DAL.App.Repositories
{
    public interface IProductInOrderRepository : IBaseRepository<ProductInOrder>
    {
        // add your ProductInOrder custom method declarations here
        Task DeleteAllByNameAsync(string name);
    }
}