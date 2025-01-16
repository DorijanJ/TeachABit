using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TeachABit.Model.ValidationAttributes;

namespace TeachABit.Model.DTOs.Tecajevi
{
    public class CreateTecajDto
    {
        public string Naziv { get; set; } = string.Empty;
        public string? VlasnikId { get; set; } = null;
        public decimal? Cijena { get; set; } = null;
        public string Opis { get; set; } = string.Empty;
        [ImageFile(maxFileSize: 5 * 1024 * 1024)]
        public IFormFile? ProfilnaSlika { get; set; }
    }
}
