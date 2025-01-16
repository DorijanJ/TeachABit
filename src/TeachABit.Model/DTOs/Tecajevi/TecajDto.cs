﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TeachABit.Model.DTOs.Tecajevi
{
    public class TecajDto
    {
        public int Id { get; set; }
        [StringLength(100, ErrorMessage = "Naslov je previše dugačak.")]
        public string Naziv { get; set; } = string.Empty;
        public bool? Favorit { get; set; } = false;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? Cijena { get; set; } = null;
        public bool isPublished{ get; set; }=false;
        public string VlasnikId { get; set; } = string.Empty;
        public string? VlasnikUsername { get; set; }
        public bool? Kupljen { get; set; } = false;
        public List<LekcijaDto>? Lekcije { get; set; } = [];
    }
}