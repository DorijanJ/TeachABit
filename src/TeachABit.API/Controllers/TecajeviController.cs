using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetTecajList()
        {
            return GetControllerResult(await _tecajeviService.GetTecajList());
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTecaj(int id)
        {
            return GetControllerResult(await _tecajeviService.GetTecaj(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTecaj(TecajDto tecaj)
        {
            return GetControllerResult(await _tecajeviService.CreateTecaj(tecaj));
        }

        /*[HttpPut]
        public async Task<IActionResult> UpdateTecaj(TecajDto tecaj)
        {
            return GetControllerResult(await _tecajeviService.UpdateTecaj(tecaj));
        }*/

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTecaj(int id)
        {
            return GetControllerResult(await _tecajeviService.DeleteTecaj(id));
        }
    }
}