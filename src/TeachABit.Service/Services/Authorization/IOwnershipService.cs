using TeachABit.Model.DTOs.Objave;
using TeachABit.Model.DTOs.Radionice;
using TeachABit.Model.DTOs.Tecajevi;
using TeachABit.Model.Models.Objave;
using TeachABit.Model.Models.Radionice;
using TeachABit.Model.Models.Tecajevi;

namespace TeachABit.Service.Services.Authorization
{
    public interface IOwnershipService
    {
        public bool Owns(ObjavaDto objava);
        public bool Owns(Objava objava);
        public bool Owns(Tecaj tecaj);
        public bool Owns(TecajDto tecaj);
        public bool Owns(Radionica radionica);
        public bool Owns(RadionicaDto radionica);
        public bool Owns(ObjavaKomentar objavaKomentar);
        public bool Owns(ObjavaKomentarDto objavaKomentar);
        public bool Owns(RadionicaKomentar radionicaKomentar);
        public bool Owns(RadionicaKomentarDto radionicaKomentar);
        public bool Owns(TecajKomentar tecajKomentar);
        public bool Owns(TecajKomentarDto tecajKomentar);
    }
}
