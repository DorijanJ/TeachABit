using AutoMapper;
using TeachABit.Model.DTOs.Tecajevi;
using TeachABit.Model.DTOs.Korisnici;
using TeachABit.Model.Models.Tecajevi;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Model.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Korisnik, KorisnikDto>().ForMember(x => x.Username, opt => opt.MapFrom(x => x.UserName));
            CreateMap<Tecaj, TecajDto>().ReverseMap();
        }
    }
}
