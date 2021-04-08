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
            GetRepository(() => new BillRepository(UowDbContext, Mapper));
        public ICompanyRepository Companies => 
            GetRepository(() => new CompanyRepository(UowDbContext, Mapper));
        public IDiscountRepository Discounts => 
            GetRepository(() => new DiscountRepository(UowDbContext, Mapper));
        public ILineOnBillRepository LinesOnBills => 
            GetRepository(() => new LineOnBillRepository(UowDbContext, Mapper));
        public IOrderRepository Orders => 
            GetRepository(() => new OrderRepository(UowDbContext, Mapper));
        public IPaymentRepository Payments => 
            GetRepository(() => new PaymentRepository(UowDbContext, Mapper));
        public IPaymentTypeRepository PaymentTypes => 
            GetRepository(() => new PaymentTypeRepository(UowDbContext, Mapper));
        public IPriceRepository Prices => 
            GetRepository(() => new PriceRepository(UowDbContext, Mapper));
        public IProductRepository Products => 
            GetRepository(() => new ProductRepository(UowDbContext, Mapper));
        public IProductInOrderRepository ProductsInOrders => 
            GetRepository(() => new ProductInOrderRepository(UowDbContext, Mapper));
        public IProductTypeRepository ProductTypes => 
            GetRepository(() => new ProductTypeRepository(UowDbContext, Mapper));
        public IWarehouseRepository Warehouses => 
            GetRepository(() => new WarehouseRepository(UowDbContext, Mapper));
        public IProductInWarehouseRepository ProductsInWarehouses => 
            GetRepository(() => new ProductInWarehouseRepository(UowDbContext, Mapper));
        

        
    }
}