using System;
using AutoMapper;
using BLL.App.Mappers;
using BLL.App.Services;
using BLL.Base;
using BLL.Base.Services;
using Contracts.BLL.App;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base.Services;
using Contracts.DAL.App;
using Contracts.DAL.Base.Repositories;
using Domain.App;

namespace BLL.App
{
    public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
    {
        protected IMapper Mapper;
        
        public AppBLL(IAppUnitOfWork uow, IMapper mapper) : base(uow)
        {
            Mapper = mapper;
            
        }


        public IPersonService Persons =>
            GetService<IPersonService>(() => new PersonService(Uow, Uow.Persons, Mapper));
        
        public IBillService Bills =>
            GetService<IBillService>(() => new BillService(Uow, Uow.Bills, Mapper));
        
        public ICompanyService Companies =>
            GetService<ICompanyService>(() => new CompanyService(Uow, Uow.Companies, Mapper));
        
        public IDiscountService Discounts =>
            GetService<IDiscountService>(() => new DiscountService(Uow, Uow.Discounts, Mapper));
        
        public ILineOnBillService LinesOnBills =>
            GetService<ILineOnBillService>(() => new LineOnBillService(Uow, Uow.LinesOnBills, Mapper));
        
        public IOrderService Orders =>
            GetService<IOrderService>(() => new OrderService(Uow, Uow.Orders, Mapper));
        
        public IPaymentService Payments =>
            GetService<IPaymentService>(() => new PaymentService(Uow, Uow.Payments, Mapper));
        
        public IPaymentTypeService PaymentTypes =>
            GetService<IPaymentTypeService>(() => new PaymentTypeService(Uow, Uow.PaymentTypes, Mapper));
        
        public IPriceService Prices =>
            GetService<IPriceService>(() => new PriceService(Uow, Uow.Prices, Mapper));
        
        public IProductService Products =>
            GetService<IProductService>(() => new ProductService(Uow, Uow.Products, Mapper));
        
        public IProductInOrderService ProductsInOrders =>
            GetService<IProductInOrderService>(() => new ProductInOrderService(Uow, Uow.ProductsInOrders, Mapper));
        
        public IProductInWarehouseService ProductsInWarehouses =>
            GetService<IProductInWarehouseService>(() => new ProductInWarehouseService(Uow, Uow.ProductsInWarehouses, Mapper));
        
        public IProductTypeService ProductTypes =>
            GetService<IProductTypeService>(() => new ProductTypeService(Uow, Uow.ProductTypes, Mapper));
        
        public IWarehouseService Warehouses =>
            GetService<IWarehouseService>(() => new WarehouseService(Uow, Uow.Warehouses, Mapper));

        public IPurchaseReceivedPageService PurchaseReceivedPageService =>
            GetService<IPurchaseReceivedPageService>(() => new PurchaseReceivedPageService(Uow, Mapper));



        /*public IContactService Contacts =>
            GetService<IContactService>(() => new ContactService(Uow, Uow.Contacts));

        public IContactTypeService ContactTypes =>
            GetService<IContactTypeService>(() => new ContactTypeService(Uow, Uow.ContactTypes));*/

        /*public IBaseEntityService<BLL.App.DTO.Discount, DAL.App.DTO.Simple> Discounts =>
            GetService<IBaseEntityService<BLL.App.DTO.Discount, DAL.App.DTO.Discount>>(()
                => new BaseEntityService<IAppUnitOfWork,
                    IBaseRepository<DAL.App.DTO.Discount>, BLL.App.DTO.Discount, DAL.App.DTO.Discount>(Uow, Uow.Discounts, new BaseMapper<BLL.App.DTO.Discount, DAL.App.DTO.Discount>(Mapper)));*/
    }
}

  