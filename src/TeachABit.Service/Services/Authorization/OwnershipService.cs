using TeachABit.Model.DTOs.Objave;
using TeachABit.Model.DTOs.Radionice;
using TeachABit.Model.DTOs.Tecajevi;
using TeachABit.Model.Models.Objave;
using TeachABit.Model.Models.Radionice;
using TeachABit.Model.Models.Tecajevi;

namespace TeachABit.Service.Services.Authorization
{
    public class OwnershipService(IAuthorizationService authorizationService) : IOwnershipService
    {
        private readonly IAuthorizationService _authorizationService = authorizationService;

        public bool Owns(ObjavaDto objava)
        {
            return objava.VlasnikId == _authorizationService.GetKorisnik().Id;
        }

        public bool Owns(Objava objava)
        {
            return objava.VlasnikId == _authorizationService.GetKorisnik().Id;
        }

        public bool Owns(Tecaj tecaj)
        {
            return tecaj.VlasnikId == _authorizationService.GetKorisnik().Id;
        }

        public bool Owns(TecajDto tecaj)
        {
            return tecaj.VlasnikId == _authorizationService.GetKorisnik().Id;
        }

        public bool Owns(ObjavaKomentar objavaKomentar)
        {
            return objavaKomentar.VlasnikId == _authorizationService.GetKorisnik().Id;
        }

        public bool Owns(ObjavaKomentarDto objavaKomentar)
        {
            return objavaKomentar.VlasnikId == _authorizationService.GetKorisnik().Id;
        }

        public bool Owns(RadionicaKomentar radionicaKomentar)
        {
            return radionicaKomentar.VlasnikId == _authorizationService.GetKorisnik().Id;
        }

        public bool Owns(RadionicaKomentarDto radionicaKomentar)
        {
            return radionicaKomentar.VlasnikId == _authorizationService.GetKorisnik().Id;
        }

        public bool Owns(TecajKomentar tecajKomentar)
        {
            return tecajKomentar.VlasnikId == _authorizationService.GetKorisnik().Id;
        }

        public bool Owns(TecajKomentarDto tecajKomentar)
        {
            return tecajKomentar.VlasnikId == _authorizationService.GetKorisnik().Id;
        }

        public bool Owns(Radionica radionica)
        {
            return radionica.VlasnikId == _authorizationService.GetKorisnik().Id;
        }

        public bool Owns(RadionicaDto radionica)
        {
            return radionica.VlasnikId == _authorizationService.GetKorisnik().Id;
        }
    }
}
