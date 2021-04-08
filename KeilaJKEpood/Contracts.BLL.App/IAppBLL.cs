using System;
using Contracts.BLL.App.Services;
using Contracts.BLL.Base;
using Contracts.BLL.Base.Services;
using BLLAppDTO = BLL.App.DTO;
using DALAppDTO = DAL.App.DTO;
namespace Contracts.BLL.App
{
    public interface IAppBLL : IBaseBLL
    {
        IPersonService Persons { get; }
        IBillService Bills { get; }
        ICompanyService Companies { get; }
        IDiscountService Discounts { get; }
        ILineOnBillService LinesOnBills { get; }
        IOrderService Orders { get; }
        IPaymentService Payments { get; }
        IPaymentTypeService PaymentTypes { get; }
        IPriceService Prices { get; }
        IProductService Products { get; }
        IProductInOrderService ProductsInOrders { get; }
        IProductInWarehouseService ProductsInWarehouses { get; }
        IProductTypeService ProductTypes { get; }
        IWarehouseService Warehouses { get; }
        
        //IContactService Contacts { get; }
        //IContactTypeService ContactTypes { get; }

        // this stays as BaseService, just for testing
        //IBaseEntityService<BLLAppDTO.Simple, DALAppDTO.Simple> Simples { get; }
    }
}