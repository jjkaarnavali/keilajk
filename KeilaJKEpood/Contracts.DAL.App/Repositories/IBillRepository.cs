using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;

namespace Contracts.DAL.App.Repositories
{
    public interface IBillRepository : IBaseRepository<Bill>
    {
        // add your Bill custom method declarations here
        Task DeleteAllByNameAsync(string name);
    }
}