using AutoMapper;
using BLL.App.DTO.Identity;


namespace BLL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            
            CreateMap<Person, DAL.App.DTO.Person>().ReverseMap();
            CreateMap<Bill, DAL.App.DTO.Bill>().ReverseMap();
            CreateMap<Company, DAL.App.DTO.Company>().ReverseMap();
            CreateMap<LineOnBill, DAL.App.DTO.LineOnBill>().ReverseMap();
            CreateMap<Order, DAL.App.DTO.Order>().ReverseMap();
            CreateMap<Payment, DAL.App.DTO.Payment>().ReverseMap();
            CreateMap<PaymentType, DAL.App.DTO.PaymentType>().ReverseMap();
            CreateMap<Price, DAL.App.DTO.Price>().ReverseMap();
            CreateMap<Product, DAL.App.DTO.Product>().ReverseMap();
            CreateMap<ProductInOrder, DAL.App.DTO.ProductInOrder>().ReverseMap();
            CreateMap<ProductInWarehouse, DAL.App.DTO.ProductInWarehouse>().ReverseMap();
            CreateMap<ProductType, DAL.App.DTO.ProductType>().ReverseMap();
            CreateMap<Warehouse, DAL.App.DTO.Warehouse>().ReverseMap();
            CreateMap<AppUser, DAL.App.DTO.Identity.AppUser>().ReverseMap();
            CreateMap<AppRole, DAL.App.DTO.Identity.AppRole>().ReverseMap();
        }
    }
}