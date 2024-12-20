﻿using AutoMapper;
using TeachABit.Model.DTOs.Korisnici;
using TeachABit.Model.DTOs.Objave;
using TeachABit.Model.DTOs.Radionice;
using TeachABit.Model.DTOs.Tecajevi;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Model.Models.Objave;
using TeachABit.Model.Models.Radionice;
using TeachABit.Model.Models.Tecajevi;

namespace TeachABit.Model.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Korisnik, KorisnikDto>().ReverseMap();
            CreateMap<Tecaj, TecajDto>().ReverseMap();
            CreateMap<ObjavaDto, Objava>();
            CreateMap<Objava, ObjavaDto>()
                .ForMember(x => x.VlasnikUsername, opt => opt.MapFrom(x => x.Vlasnik.UserName))
                .ForMember(x => x.VlasnikProfilnaSlikaVersion, opt => opt.MapFrom(x => x.Vlasnik.ProfilnaSlikaVersion))
                .Include<Objava, DetailedObjavaDto>();
            CreateMap<Objava, DetailedObjavaDto>();
            CreateMap<Komentar, KomentarDto>()
                .ForMember(x => x.VlasnikUsername, opt => opt.MapFrom(x => x.Vlasnik.UserName));
            CreateMap<Radionica, RadionicaDto>().ReverseMap();
        }
    }
}
