using AutoMapper;

namespace DTO.App.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BLL.App.DTO.Person, DTO.App.PersonAdd>().ReverseMap();
            CreateMap<BLL.App.DTO.Person, DTO.App.PersonDTO>().ReverseMap();
            CreateMap<BLL.App.DTO.LineOnBill, DTO.App.LineOnBillDTO>().ReverseMap();
            CreateMap<BLL.App.DTO.Bill, DTO.App.BillDTO>().ReverseMap();
            CreateMap<BLL.App.DTO.Company, DTO.App.CompanyDTO>().ReverseMap();
            CreateMap<BLL.App.DTO.Discount, DTO.App.DiscountDTO>().ReverseMap();
            CreateMap<BLL.App.DTO.Order, DTO.App.OrderDTO>().ReverseMap();
            CreateMap<BLL.App.DTO.Payment, DTO.App.PaymentDTO>().ReverseMap();
            CreateMap<BLL.App.DTO.PaymentType, DTO.App.PaymentTypeDTO>().ReverseMap();
            CreateMap<BLL.App.DTO.Price, DTO.App.PriceDTO>().ReverseMap();
            CreateMap<BLL.App.DTO.Product, DTO.App.ProductDTO>().ReverseMap();
            CreateMap<BLL.App.DTO.ProductInOrder, DTO.App.ProductInOrderDTO>().ReverseMap();
            CreateMap<BLL.App.DTO.ProductInWarehouse, DTO.App.ProductInWarehouseDTO>().ReverseMap();
            CreateMap<BLL.App.DTO.ProductType, DTO.App.ProductTypeDTO>().ReverseMap();
            CreateMap<BLL.App.DTO.Warehouse, DTO.App.WarehouseDTO>().ReverseMap();
            CreateMap<BLL.App.DTO.Identity.AppUser, DTO.App.AppUserDTO>().ReverseMap();
        }

    }
}