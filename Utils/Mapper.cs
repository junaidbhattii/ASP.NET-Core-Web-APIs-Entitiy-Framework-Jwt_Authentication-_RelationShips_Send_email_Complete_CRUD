using AutoMapper;
using JwtAuthentication_Relations_Authorization.DTO;
using JwtAuthentication_Relations_Authorization.Models;

namespace JwtAuthentication_Relations_Authorization.Utils
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<User , VendorBodyRequest>().ReverseMap();
            CreateMap<User,vendorResponse>().ReverseMap();
            CreateMap<Vendor,VendorBodyRequest>().ReverseMap(); 
            CreateMap<User,UserResponce>().ReverseMap();
            CreateMap<Role,RoleResponse>().ReverseMap();

            CreateMap<Vendor, vendorResponse>()
                .ForMember(o => o.user, opt => opt.MapFrom(src => src.user))
                .ForPath(dets => dets.user.Role, opt => opt.MapFrom(src => src.user.Role));
        }
    }
}
