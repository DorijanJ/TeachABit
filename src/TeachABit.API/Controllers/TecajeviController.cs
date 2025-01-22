using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachABit.API.Middleware;
using TeachABit.Model.DTOs.Tecajevi;
using TeachABit.Service.Services.Tecajevi;

namespace TeachABit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TecajeviController(ITecajeviService tecajeviService) : BaseController
    {
        private readonly ITecajeviService _tecajeviService = tecajeviService;

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetTecajList(string? search = null)
        {
            var result = await _tecajeviService.GetTecajList(search);
            return GetControllerResult(result);
        }
        [AllowAnonymous]
        [HttpGet("filter-by-price")]
        public async Task<IActionResult> GetTecajListByFiltratingCijena(int maxCijena, int minCijena)
        {
            var result = await _tecajeviService.GetTecajListByFiltratingCijena(maxCijena, minCijena);
            return GetControllerResult(result);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTecaj(int id)
        {
            return GetControllerResult(await _tecajeviService.GetTecaj(id));
        }

        [ModelStateFilter]
        [HttpPost]
        public async Task<IActionResult> CreateTecaj([FromBody] CreateOrUpdateTecajDto tecaj)
        {
            return GetControllerResult(await _tecajeviService.CreateTecaj(tecaj));
        }

        [ModelStateFilter]
        [HttpPut]
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
        public async Task<IActionResult> UpdateLekcija(UpdatedLekcijaDto updateLekcija)
        {
            return GetControllerResult(await _tecajeviService.UpdateLekcija(updateLekcija));
        }

        [HttpDelete("lekcije/{lekcijaId}")]
        public async Task<IActionResult> DeleteLekcija(int lekcijaId)
        {
            return GetControllerResult(await _tecajeviService.DeleteLekcija(lekcijaId));
        }

        [HttpPost("{id}/lekcije")]
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
        public async Task<IActionResult> CreateKomentarTecaj([FromBody] KomentarTecajDto komentar, int id)
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
        public async Task<IActionResult> UpdateKomentarTecaj(UpdateKomentarTecajDto updateKomentarTecaj)
        {
            return GetControllerResult(await _tecajeviService.UpdateKomentarTecaj(updateKomentarTecaj));
        }

        [HttpDelete("komentari/{komentarId}")]
        public async Task<IActionResult> DeleteKomentarTecaj(int komentarId)
        {
            return GetControllerResult(await _tecajeviService.DeleteKomentarTecaj(komentarId));
        }
    }
}