﻿using AutoMapper;
using TeachABit.Model.DTOs.Authentication;
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
            CreateMap<Korisnik, RefreshUserInfoDto>()
                .ForMember(x => x.Roles, opt => opt.MapFrom(x => x.KorisnikUloge.Select(x => x.Uloga)));
            CreateMap<Korisnik, KorisnikDto>()
                .ForMember(x => x.KorisnikStatus, opt => opt.MapFrom(x => x.KorisnikStatus != null ? x.KorisnikStatus.Naziv : null))
                .ForMember(x => x.Roles, opt => opt.MapFrom(x => x.KorisnikUloge.Select(x => x.Uloga)))
                .ForMember(x => x.VerifikacijaStatusNaziv, opt => opt.MapFrom(x => x.VerifikacijaStatus != null ? x.VerifikacijaStatus.Naziv : null));
            CreateMap<KorisnikDto, Korisnik>();
            CreateMap<Tecaj, TecajDto>()
                .ForMember(x => x.VlasnikUsername, opt => opt.MapFrom(x => x.Vlasnik.UserName))
                .ForMember(x => x.Kupljen, opt => opt.MapFrom(x => x.TecajPlacanja.Count > 0))
                .ForMember(x => x.VlasnikProfilnaSlikaVersion, opt => opt.MapFrom(x => x.Vlasnik.ProfilnaSlikaVersion))
                .ForMember(x => x.Favorit, opt => opt.MapFrom(x => x.KorisnikTecajFavoriti.Any()))
                .ForMember(x => x.Ocjena, opt => opt.MapFrom(x => x.KorisnikTecajOcjene.Count > 0 ? x.KorisnikTecajOcjene.Select(x => x.Ocjena).Sum() / x.KorisnikTecajOcjene.Count : 0));
            CreateMap<TecajDto, Tecaj>();
            CreateMap<CreateOrUpdateTecajDto, Tecaj>();
            CreateMap<CreateOrUpdateRadionicaDto, Radionica>();
            CreateMap<Lekcija, LekcijaDto>();
            CreateMap<LekcijaDto, Lekcija>();
            CreateMap<ObjavaDto, Objava>();
            CreateMap<Objava, ObjavaDto>()
                .ForMember(x => x.VlasnikUsername, opt => opt.MapFrom(x => x.Vlasnik.UserName))
                .ForMember(x => x.LikeCount, opt => opt.MapFrom(x => x.ObjavaReakcijaList.Select(x => x.Liked ? 1 : -1).Sum()))
                .ForMember(x => x.VlasnikProfilnaSlikaVersion, opt => opt.MapFrom(x => x.Vlasnik.ProfilnaSlikaVersion));
            CreateMap<ObjavaKomentarDto, ObjavaKomentar>();
            CreateMap<ObjavaKomentar, ObjavaKomentarDto>()
                .ForMember(x => x.VlasnikProfilnaSlikaVersion, opt => opt.MapFrom(x => x.Vlasnik.ProfilnaSlikaVersion))
                .ForMember(x => x.VlasnikUsername, opt => opt.MapFrom(x => x.Vlasnik.UserName))
                .ForMember(x => x.Sadrzaj, opt => opt.MapFrom(x => x.IsDeleted ? "[Izbrisan]" : x.Sadrzaj))
                .ForMember(x => x.LikeCount, opt => opt.MapFrom(x => x.KomentarReakcijaList.Select(x => x.Liked ? 1 : -1).Sum()))
                .ForMember(x => x.NadKomentarId, opt => opt.MapFrom(x => x.NadKomentarId));
            CreateMap<Radionica, RadionicaDto>()
                .ForMember(x => x.VlasnikProfilnaSlikaVersion, opt => opt.MapFrom(x => x.Vlasnik.ProfilnaSlikaVersion))
                .ForMember(x => x.VlasnikUsername, opt => opt.MapFrom(x => x.Vlasnik.UserName))
                .ForMember(x => x.Placen, opt => opt.MapFrom(x => x.Placanja.Any()))
                .ForMember(x => x.Favorit, opt => opt.MapFrom(x => x.RadionicaFavoriti.Count > 0))
                .ForMember(x => x.Ocjena,
                    opt => opt.MapFrom((x) =>
                        x.Ocjene.Count > 0 ? x.Ocjene.Select(x => x.Ocjena).Sum() / x.Ocjene.Count : 0))
                .ForMember(
                    x => x.BrojPrijavljenih,
                    opt => opt.MapFrom(x => x.Placanja.Count));
            CreateMap<RadionicaDto, Radionica>();
            CreateMap<Uloga, UlogaDto>();
            CreateMap<RadionicaKomentarDto, RadionicaKomentar>();
            CreateMap<RadionicaKomentar, RadionicaKomentarDto>()
                .ForMember(x => x.VlasnikProfilnaSlikaVersion, opt => opt.MapFrom(x => x.Vlasnik.ProfilnaSlikaVersion))
                .ForMember(x => x.VlasnikUsername, opt => opt.MapFrom(x => x.Vlasnik.UserName))
                .ForMember(x => x.Sadrzaj, opt => opt.MapFrom(x => x.IsDeleted ? "[Izbrisan]" : x.Sadrzaj))
                .ForMember(x => x.LikeCount, opt => opt.MapFrom(x => x.KomentarRadionicaReakcijaList.Select(x => x.Liked ? 1 : -1).Sum()))
                .ForMember(x => x.NadKomentarId, opt => opt.MapFrom(x => x.NadKomentarId));
            CreateMap<TecajKomentar, TecajKomentarDto>()
                .ForMember(x => x.VlasnikProfilnaSlikaVersion, opt => opt.MapFrom(x => x.Vlasnik.ProfilnaSlikaVersion))
                .ForMember(x => x.VlasnikUsername, opt => opt.MapFrom(x => x.Vlasnik.UserName))
                .ForMember(x => x.Sadrzaj, opt => opt.MapFrom(x => x.IsDeleted ? "[Izbrisan]" : x.Sadrzaj))
                .ForMember(x => x.LikeCount, opt => opt.MapFrom(x => x.KomentarReakcijaList.Select(x => x.Liked ? 1 : -1).Sum()))
                .ForMember(x => x.NadKomentarId, opt => opt.MapFrom(x => x.NadKomentarId));
            CreateMap<TecajKomentarDto, TecajKomentar>();
        }
    }
}
