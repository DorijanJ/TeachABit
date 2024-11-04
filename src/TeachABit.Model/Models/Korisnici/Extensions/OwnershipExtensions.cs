using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeachABit.Model.DTOs.Objave;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Model.Models.Korisnici.Extensions
{
    public static class OwnershipExtensions
    {
        public static bool Owns(this Korisnik korisnik, ObjavaDto objava)
        {
            return objava.VlasnikId == korisnik.Id;
        }
    }
}
