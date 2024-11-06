using AutoMapper;
using TeachABit.Model.DTOs.Radionice;
using TeachABit.Model.DTOs.Tecajevi;
using TeachABit.Model.DTOs.User;
using TeachABit.Model.Models.Radionice;
using TeachABit.Model.Models.Tecajevi;
using TeachABit.Model.Models.User;

namespace TeachABit.Model.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, AppUserDto>().ForMember(x => x.Username, opt => opt.MapFrom(x => x.UserName));
            CreateMap<Tecaj, TecajDto>().ReverseMap();
            CreateMap<Radionica, RadionicaDto>().ReverseMap();
        }
    }
}
