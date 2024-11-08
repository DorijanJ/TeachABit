﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeachABit.Model.Models.Korisnici;

namespace TeachABit.Model.Models.Objave
{
    [Table("Objava")]
    public class Objava
    {
        [Key]
        public int Id { get; set; }
        public string Naziv {  get; set; } = string.Empty;
        public string Sadrzaj { get; set; } = string.Empty;
        public string VlasnikId { get; set; } = string.Empty;
        [ForeignKey(nameof(VlasnikId))]
        public required Korisnik Vlasnik { get; set; }
    }
}
