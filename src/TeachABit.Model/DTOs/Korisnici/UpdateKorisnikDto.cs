﻿namespace TeachABit.Model.DTOs.Korisnici
{
    public class UpdateKorisnikDto
    {

        public string? Username { get; set; } = string.Empty;

        [ImageFile(maxFileSize: 5 * 1024 * 1024)]
        public string? ProfilnaSlikaBase64 { get; set; }
    }
}
