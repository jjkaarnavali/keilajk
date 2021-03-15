using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;
using DAL.Base.EF.Repositories;
using Domain.App;

namespace DAL.App.EF
{
    public class AppUnitOfWork : BaseUnitOfWork<AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
            // TODO: replace this trivial factory with something more decent
            Persons = new PersonRepository(uowDbContext);
            Bills = new BillRepository(uowDbContext);
            Companies = new CompanyRepository(uowDbContext);
            Discounts = new DiscountRepository(uowDbContext);
            LinesOnBills = new LineOnBillRepository(uowDbContext);
            Orders = new OrderRepository(uowDbContext);
            Payments = new PaymentRepository(uowDbContext);
            PaymentTypes = new PaymentTypeRepository(uowDbContext);
            Prices = new PriceRepository(uowDbContext);
            Products = new ProductRepository(uowDbContext);
            ProductsInOrders = new ProductInOrderRepository(uowDbContext);
            ProductsInWarehouses = new ProductInWarehouseRepository(uowDbContext);
            ProductTypes = new ProductTypeRepository(uowDbContext);
            Warehouses = new WarehouseRepository(uowDbContext);

        }

        public IPersonRepository Persons { get; }
        public IBillRepository Bills { get; }
        public ICompanyRepository Companies { get; }
        public IDiscountRepository Discounts { get; }
        public ILineOnBillRepository LinesOnBills { get; }
        public IOrderRepository Orders { get; }
        public IPaymentRepository Payments { get; }
        public IPaymentTypeRepository PaymentTypes { get; }
        public IPriceRepository Prices { get; }
        public IProductRepository Products { get; }
        public IProductInOrderRepository ProductsInOrders { get; }
        public IProductInWarehouseRepository ProductsInWarehouses { get; }
        public IProductTypeRepository ProductTypes { get; }
        public IWarehouseRepository Warehouses { get; }
        
    }
}