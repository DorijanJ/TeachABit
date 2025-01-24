using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachABit.API.Middleware;
using TeachABit.Model.DTOs.Placanja;
using TeachABit.Model.DTOs.Tecajevi;
using TeachABit.Service.Services.Placanja;
using TeachABit.Service.Services.Tecajevi;

namespace TeachABit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TecajeviController(ITecajeviService tecajeviService, IPlacanjaService placanjaService) : BaseController
    {
        private readonly ITecajeviService _tecajeviService = tecajeviService;
        private readonly IPlacanjaService _placanjaService = placanjaService;

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetTecajList(string? search = null, string? vlasnikUsername = null, decimal? minCijena = null, decimal? maxCijena = null)
        {
            var result = await _tecajeviService.GetTecajList(search, vlasnikUsername, minCijena, maxCijena);
            return GetControllerResult(result);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTecaj(int id)
        {
            return GetControllerResult(await _tecajeviService.GetTecaj(id));
        }

        [HttpPost]
        [ModelStateFilter]
        public async Task<IActionResult> CreateTecaj([FromBody] CreateOrUpdateTecajDto tecaj)
        {
            return GetControllerResult(await _tecajeviService.CreateTecaj(tecaj));
        }

        [HttpPut]
        [ModelStateFilter]
        public async Task<IActionResult> UpdateTecaj(CreateOrUpdateTecajDto tecaj)
        {
            return GetControllerResult(await _tecajeviService.UpdateTecaj(tecaj));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTecaj(int id)
        {
            return GetControllerResult(await _tecajeviService.DeleteTecaj(id));
        }

        [AllowAnonymous]
        [HttpGet("lekcije")]
        public async Task<IActionResult> GetLekcijeList(string? search = null)
        {
            var result = await _tecajeviService.GetLekcijaList(search);
            return GetControllerResult(result);
        }

        [HttpPut("lekcije")]
        [ModelStateFilter]
        public async Task<IActionResult> UpdateLekcija(UpdateLekcijaDto updateLekcija)
        {
            return GetControllerResult(await _tecajeviService.UpdateLekcija(updateLekcija));
        }

        [HttpDelete("lekcije/{lekcijaId}")]
        public async Task<IActionResult> DeleteLekcija(int lekcijaId)
        {
            return GetControllerResult(await _tecajeviService.DeleteLekcija(lekcijaId));
        }

        [HttpPost("{id}/lekcije")]
        [ModelStateFilter]
        public async Task<IActionResult> CreateLekcija([FromBody] LekcijaDto lekcija, int id)
        {
            return GetControllerResult(await _tecajeviService.CreateLekcija(lekcija, id));

        }

        [AllowAnonymous]
        [HttpGet("{id}/komentari")]
        public async Task<IActionResult> GetKomentarTecajListByTecajId(int id)
        {
            return GetControllerResult(await _tecajeviService.GetKomentarTecajListRecursive(id));
        }

        [HttpPost("{id}/komentari")]
        [ModelStateFilter]
        public async Task<IActionResult> CreateKomentarTecaj([FromBody] TecajKomentarDto komentar, int id)
        {
            return GetControllerResult(await _tecajeviService.CreateKomentarTecaj(komentar, id));
        }

        [HttpPost("komentari/{komentarId}/like")]
        public async Task<IActionResult> Like(int komentarId)
        {
            return GetControllerResult(await _tecajeviService.LikeKomentarTecaj(komentarId));
        }

        [HttpPost("komentari/{komentarId}/dislike")]
        public async Task<IActionResult> DislikeKomentarTecaj(int komentarId)
        {
            return GetControllerResult(await _tecajeviService.DislikeKomentarTecaj(komentarId));
        }

        [HttpDelete("komentari/{komentarId}/reakcija")]
        public async Task<IActionResult> ClearKomentarTecajReaction(int komentarId)
        {
            return GetControllerResult(await _tecajeviService.ClearKomentarTecajReaction(komentarId));
        }

        [HttpPut("komentari")]
        [ModelStateFilter]
        public async Task<IActionResult> UpdateKomentarTecaj(UpdateKomentarTecajDto updateKomentarTecaj)
        {
            return GetControllerResult(await _tecajeviService.UpdateKomentarTecaj(updateKomentarTecaj));
        }

        [HttpDelete("komentari/{komentarId}")]
        public async Task<IActionResult> DeleteKomentarTecaj(int komentarId)
        {
            return GetControllerResult(await _tecajeviService.DeleteKomentarTecaj(komentarId));
        }

        [HttpPost("{tecajId}/ocjena")]
        public async Task<IActionResult> CreateTecajOcjena(int tecajId, [FromQuery] int ocjena)
        {
            return GetControllerResult(await _tecajeviService.CreateTecajOcjena(tecajId, ocjena));
        }

        [HttpDelete("{tecajId}/ocjena")]
        public async Task<IActionResult> DeleteTecajOcjena(int tecajId)
        {
            return GetControllerResult(await _tecajeviService.DeleteTecajOcjena(tecajId));

        }
        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] TecajPlacanjeRequestDto request)
        {
            return GetControllerResult(await _placanjaService.CreateTecajCheckoutSession(request));
        }

        public async Task<IActionResult> GetOcjena(int id)
        {
            return GetControllerResult(await _tecajeviService.GetOcjena(id));
        }
    }
}