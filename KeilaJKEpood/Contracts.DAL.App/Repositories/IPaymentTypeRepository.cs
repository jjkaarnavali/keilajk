using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using Domain.App;

namespace Contracts.DAL.App.Repositories
{
    public interface IPaymentTypeRepository : IBaseRepository<PaymentType>
    {
        // add your PaymentType custom method declarations here
        Task DeleteAllByNameAsync(string name);
    }
}