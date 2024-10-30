using AutoMapper;
using TeachABit.Model.DTOs.Tecaj;
using TeachABit.Model.DTOs.User;
using TeachABit.Model.Models.Tecaj;
using TeachABit.Model.Models.User;

namespace TeachABit.Model.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, AppUserDto>().ForMember(x => x.UserName, opt => opt.MapFrom(x => x.UserName));
            CreateMap<Tecaj, TecajDto>().ReverseMap();
        }
    }
}
