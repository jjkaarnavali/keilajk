using AutoMapper;
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
        protected IMapper Mapper;
        public AppUnitOfWork(AppDbContext uowDbContext, IMapper mapper) : base(uowDbContext)
        {
            Mapper = mapper;
        }


        
        /*public IPersonRepository Persons { get; }
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
        public IWarehouseRepository Warehouses { get; }*/
        
        public IPersonRepository Persons => 
            GetRepository(() => new PersonRepository(UowDbContext, Mapper));

        public IBillRepository Bills => 
            GetRepository(() => new BillRepository(UowDbContext));
        public ICompanyRepository Companies => 
            GetRepository(() => new CompanyRepository(UowDbContext));
        public IDiscountRepository Discounts => 
            GetRepository(() => new DiscountRepository(UowDbContext));
        public ILineOnBillRepository LinesOnBills => 
            GetRepository(() => new LineOnBillRepository(UowDbContext));
        public IOrderRepository Orders => 
            GetRepository(() => new OrderRepository(UowDbContext));
        public IPaymentRepository Payments => 
            GetRepository(() => new PaymentRepository(UowDbContext));
        public IPaymentTypeRepository PaymentTypes => 
            GetRepository(() => new PaymentTypeRepository(UowDbContext));
        public IPriceRepository Prices => 
            GetRepository(() => new PriceRepository(UowDbContext));
        public IProductRepository Products => 
            GetRepository(() => new ProductRepository(UowDbContext));
        public IProductInOrderRepository ProductsInOrders => 
            GetRepository(() => new ProductInOrderRepository(UowDbContext));
        public IProductTypeRepository ProductTypes => 
            GetRepository(() => new ProductTypeRepository(UowDbContext));
        public IWarehouseRepository Warehouses => 
            GetRepository(() => new WarehouseRepository(UowDbContext));
        public IProductInWarehouseRepository ProductsInWarehouses => 
            GetRepository(() => new ProductInWarehouseRepository(UowDbContext));
        

        
    }
}