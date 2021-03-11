using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;

namespace Contracts.DAL.App.Repositories
{
    public interface IDiscountRepository : IBaseRepository<Discount>
    {
        // add your Discount custom method declarations here
        Task DeleteAllByNameAsync(string name);
    }
}