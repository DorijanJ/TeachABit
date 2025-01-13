﻿using TeachABit.Model.DTOs.Uloge;

namespace TeachABit.Model.DTOs.Korisnici
{
    public class KorisnikDto
    {
        public string Username { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string ProfilnaSlikaVersion { get; set; } = string.Empty;
        public List<UlogaDto> Roles { get; set; } = [];
        public bool Verificiran { get; set; } = false;
    }
}
