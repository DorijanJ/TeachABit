using TeachABit.Model.DTOs.Objave;
using TeachABit.Model.DTOs.Radionice;
using TeachABit.Model.DTOs.Tecajevi;
using TeachABit.Model.Models.Objave;
using TeachABit.Model.Models.Radionice;
using TeachABit.Model.Models.Tecajevi;

namespace TeachABit.Model.Models.Korisnici.Extensions
{
    public static class OwnershipExtensions
    {
        public static bool Owns(this Korisnik korisnik, ObjavaDto objava)
        {
            return objava.VlasnikId == korisnik.Id;
        }
        public static bool Owns(this Korisnik korisnik, TecajDto tecaj)
        {
            return tecaj.VlasnikId == korisnik.Id;
        }

        public static bool Owns(this Korisnik korisnik, Tecaj tecaj)
        {
            return tecaj.VlasnikId == korisnik.Id;
        }
        public static bool Owns(this Korisnik korisnik, Objava objava)
        {
            return objava.VlasnikId == korisnik.Id;
        }

        public static bool Owns(this Korisnik korisnik, KomentarDto komentar)
        {
            return komentar.VlasnikId == korisnik.Id;
        }

        public static bool Owns(this Korisnik korisnik, Komentar komentar)
        {
            return komentar.VlasnikId == korisnik.Id;
        }

        public static bool Owns(this Korisnik korisnik, KomentarRadionica komentarRadionica)
        {
            return komentarRadionica.VlasnikId == korisnik.Id;
        }

        public static bool Owns(this Korisnik korisnik, RadionicaDto radionica)
        {
            return radionica.VlasnikId == korisnik.Id;
        }

        public static bool Owns(this Korisnik korisnik, Radionica radionica)
        {
            return radionica.VlasnikId == korisnik.Id;
        }
    }
}
