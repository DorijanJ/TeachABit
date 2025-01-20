﻿using AutoMapper;
using TeachABit.Model.DTOs.Korisnici;
using TeachABit.Model.DTOs.Objave;
using TeachABit.Model.DTOs.Radionice;
using TeachABit.Model.DTOs.Tecajevi;
using TeachABit.Model.DTOs.Uloge;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Model.Models.Objave;
using TeachABit.Model.Models.Radionice;
using TeachABit.Model.Models.Tecajevi;
using TeachABit.Model.Models.Uloge;

namespace TeachABit.Model.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Korisnik, KorisnikDto>().ReverseMap();
            CreateMap<Tecaj, TecajDto>()
                .ForMember(x => x.VlasnikUsername, opt => opt.MapFrom(x => x.Vlasnik.UserName))
                .ForMember(x => x.Kupljen, opt => opt.MapFrom(x => x.TecajPlacanja.Count > 0))
                .ForMember(x => x.VlasnikProfilnaSlikaVersion, opt => opt.MapFrom(x => x.Vlasnik.ProfilnaSlikaVersion));
            CreateMap<TecajDto, Tecaj>();
            CreateMap<CreateOrUpdateTecajDto, Tecaj>();
            CreateMap<ObjavaDto, Objava>();
            CreateMap<Objava, ObjavaDto>()
                .ForMember(x => x.VlasnikUsername, opt => opt.MapFrom(x => x.Vlasnik.UserName))
                .ForMember(x => x.LikeCount, opt => opt.MapFrom(x => x.ObjavaReakcijaList.Select(x => x.Liked ? 1 : -1).Sum()))
                .ForMember(x => x.VlasnikProfilnaSlikaVersion, opt => opt.MapFrom(x => x.Vlasnik.ProfilnaSlikaVersion));
            CreateMap<KomentarDto, Komentar>();
            CreateMap<Komentar, KomentarDto>()
                .ForMember(x => x.VlasnikProfilnaSlikaVersion, opt => opt.MapFrom(x => x.Vlasnik.ProfilnaSlikaVersion))
                .ForMember(x => x.VlasnikUsername, opt => opt.MapFrom(x => x.Vlasnik.UserName))
                .ForMember(x => x.Sadrzaj, opt => opt.MapFrom(x => x.IsDeleted ? "[Removed]" : x.Sadrzaj))
                .ForMember(x => x.LikeCount, opt => opt.MapFrom(x => x.KomentarReakcijaList.Select(x => x.Liked ? 1 : -1).Sum()))
                .ForMember(x => x.NadKomentarId, opt => opt.MapFrom(x => x.NadKomentarId));
            CreateMap<Radionica, RadionicaDto>()
                .ForMember(x => x.VlasnikProfilnaSlikaVersion, opt => opt.MapFrom(x => x.Vlasnik.ProfilnaSlikaVersion))
                .ForMember(x => x.VlasnikUsername, opt => opt.MapFrom(x => x.Vlasnik.UserName));
            CreateMap<RadionicaDto, Radionica>();
            CreateMap<Uloga, UlogaDto>();
            CreateMap<KomentarRadionicaDto, KomentarRadionica>();
            CreateMap<KomentarRadionica, KomentarRadionicaDto>()
                .ForMember(x => x.VlasnikProfilnaSlikaVersion, opt => opt.MapFrom(x => x.Vlasnik.ProfilnaSlikaVersion))
                .ForMember(x => x.VlasnikUsername, opt => opt.MapFrom(x => x.Vlasnik.UserName))
                .ForMember(x => x.Sadrzaj, opt => opt.MapFrom(x => x.IsDeleted ? "[Removed]" : x.Sadrzaj))
                //.ForMember(x => x.LikeCount, opt => opt.MapFrom(x => x.KomentarReakcijaList.Select(x => x.Liked ? 1 : -1).Sum()))
                .ForMember(x => x.NadKomentarId, opt => opt.MapFrom(x => x.NadKomentarId));
        }
    }
}
