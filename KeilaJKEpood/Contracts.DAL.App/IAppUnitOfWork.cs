using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Repositories;
using Domain.App;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork : IBaseUnitOfWork
    {
        IPersonRepository Persons { get; }
        IBillRepository Bills { get; }
        ICompanyRepository Companies { get; }
        IDiscountRepository Discounts { get; }
        ILineOnBillRepository LinesOnBills { get; }
        IOrderRepository Orders { get; }
        IPaymentRepository Payments { get; }
        IPaymentTypeRepository PaymentTypes { get; }
        IPriceRepository Prices { get; }
        IProductRepository Products { get; }
        IProductInOrderRepository ProductsInOrders { get; }
        IProductInWarehouseRepository ProductsInWarehouses { get; }
        IProductTypeRepository ProductTypes { get; }
        IWarehouseRepository Warehouses { get; }
        
    }
}