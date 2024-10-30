using AutoMapper;
using TeachABit.Model.DTOs.User;
using TeachABit.Model.Models.User;

namespace TeachABit.Model.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AppUser, AppUserDto>().ForMember(x => x.Username, opt => opt.MapFrom(x => x.UserName));
        }
    }
}
