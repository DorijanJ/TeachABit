using TeachABit.Model.DTOs.Objave;
using TeachABit.Model.Models.Objave;

namespace TeachABit.Model.Models.Korisnici.Extensions
{
    public static class OwnershipExtensions
    {
        public static bool Owns(this Korisnik korisnik, ObjavaDto objava)
        {
            return objava.VlasnikId == korisnik.Id;
        }

        public static bool Owns(this Korisnik korisnik, Objava objava)
        {
            return objava.VlasnikId == korisnik.Id;
        }

        public static bool Owns(this Korisnik korisnik, KomentarDto komentar)
        {
            return komentar.VlasnikId == korisnik.Id;
        }
    }
}
