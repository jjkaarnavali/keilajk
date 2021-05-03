using AutoMapper;

namespace DAL.App.DTO.MappingProfiles
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            // CreateMap<string, Domain.Base.LangString>().ReverseMap();
            
            CreateMap<DAL.App.DTO.Person, Domain.App.Person>().ReverseMap();
            CreateMap<DAL.App.DTO.Bill, Domain.App.Bill>().ReverseMap();
            CreateMap<DAL.App.DTO.Company, Domain.App.Company>().ReverseMap();
            CreateMap<DAL.App.DTO.Discount, Domain.App.Discount>().ReverseMap();
            CreateMap<DAL.App.DTO.LineOnBill, Domain.App.LineOnBill>().ReverseMap();
            CreateMap<DAL.App.DTO.Order, Domain.App.Order>().ReverseMap();
            CreateMap<DAL.App.DTO.Payment, Domain.App.Payment>().ReverseMap();
            CreateMap<DAL.App.DTO.PaymentType, Domain.App.PaymentType>().ReverseMap();
            CreateMap<DAL.App.DTO.Price, Domain.App.Price>().ReverseMap();
            CreateMap<DAL.App.DTO.Product, Domain.App.Product>().ReverseMap();
            CreateMap<DAL.App.DTO.ProductInOrder, Domain.App.ProductType>().ReverseMap();
            CreateMap<DAL.App.DTO.ProductInWarehouse, Domain.App.ProductInWarehouse>().ReverseMap();
            CreateMap<DAL.App.DTO.ProductType, Domain.App.ProductType>().ReverseMap();
            CreateMap<DAL.App.DTO.Warehouse, Domain.App.Warehouse>().ReverseMap();
            
            CreateMap<DAL.App.DTO.Identity.AppUser, Domain.App.Identity.AppUser>().ReverseMap();
            CreateMap<DAL.App.DTO.Identity.AppRole, Domain.App.Identity.AppRole>().ReverseMap();
        }
    }
}