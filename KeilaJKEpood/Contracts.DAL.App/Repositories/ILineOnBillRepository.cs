using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;

namespace Contracts.DAL.App.Repositories
{
    public interface ILineOnBillRepository : IBaseRepository<LineOnBill>
    {
        // add your Person custom method declarations here
        Task DeleteAllByNameAsync(string name);
    }
}