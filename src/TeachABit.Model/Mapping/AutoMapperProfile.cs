using AutoMapper;
using TeachABit.Model.DTOs.Tecajevi;
using TeachABit.Model.DTOs.User;
using TeachABit.Model.Models.Tecaj;
using TeachABit.Model.Models.User;

namespace TeachABit.Model.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Korisnik, AppUserDto>().ForMember(x => x.Username, opt => opt.MapFrom(x => x.UserName));
            CreateMap<Tecaj, TecajDto>().ReverseMap();
        }
    }
}
