using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;

namespace Contracts.DAL.App.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        // add your Product custom method declarations here
        Task DeleteAllByNameAsync(string name);
    }
}