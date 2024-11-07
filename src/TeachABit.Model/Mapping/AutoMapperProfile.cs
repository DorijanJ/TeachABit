using AutoMapper;
using TeachABit.Model.DTOs.Korisnici;
using TeachABit.Model.DTOs.Objave;
using TeachABit.Model.DTOs.Tecajevi;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Model.Models.Objave;
using TeachABit.Model.Models.Tecajevi;

namespace TeachABit.Model.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Korisnik, KorisnikDto>().ForMember(x => x.Username, opt => opt.MapFrom(x => x.UserName));
            CreateMap<Tecaj, TecajDto>().ReverseMap();
            CreateMap<ObjavaDto, Objava>();
            CreateMap<Objava, ObjavaDto>()
                .ForMember(x => x.VlasnikUsername, opt => opt.MapFrom(x => x.Vlasnik.UserName))
                .Include<Objava, DetailedObjavaDto>();
            CreateMap<Objava, DetailedObjavaDto>();
            CreateMap<Komentar, KomentarDto>()
                .ForMember(x => x.VlasnikUsername, opt => opt.MapFrom(x => x.Vlasnik.UserName));
        }
    }
}
