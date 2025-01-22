using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachABit.API.Middleware;
using TeachABit.Model.DTOs.Radionice;
using TeachABit.Service.Services.Radionice;

namespace TeachABit.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RadioniceController(IRadioniceService radioniceService) : BaseController
{
    private readonly IRadioniceService _radioniceService = radioniceService;

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetRadionicaList(string? search = null, string? vlasnikUsername = null)
    {
        var result = await _radioniceService.GetRadionicaList(search, vlasnikUsername);
        return GetControllerResult(result);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRadionica(int id)
    {
        return GetControllerResult(await _radioniceService.GetRadionica(id));
    }

    [HttpPost]
    [ModelStateFilter]
    public async Task<IActionResult> CreateRadionica(CreateOrUpdateRadionicaDto radionica)
    {
        return GetControllerResult(await _radioniceService.CreateRadionica(radionica));
    }

    [HttpPut]
    [ModelStateFilter]
    public async Task<IActionResult> UpdateRadionica(CreateOrUpdateRadionicaDto radionica)
    {
        return GetControllerResult(await _radioniceService.UpdateRadionica(radionica));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRadionica(int id)
    {
        return GetControllerResult(await _radioniceService.DeleteRadionica(id));
    }

    [AllowAnonymous]
    [HttpGet("{id}/komentari")]
    public async Task<IActionResult> GetKomentarListByObjavaId(int id)
    {
        return GetControllerResult(await _radioniceService.GetKomentarListRecursive(id));
    }

    [HttpPost("{id}/komentari")]
    [ModelStateFilter]
    public async Task<IActionResult> CreateKomentar([FromBody] RadionicaKomentarDto komentar, int id)
    {
        return GetControllerResult(await _radioniceService.CreateKomentar(komentar, id));
    }

    [HttpPut("komentari")]
    [ModelStateFilter]
    public async Task<IActionResult> UpdateKomentar(UpdateKomentarRadionicaDto updateKomentar)
    {
        return GetControllerResult(await _radioniceService.UpdateKomentar(updateKomentar));
    }

    [HttpDelete("komentari/{komentarId}")]
    public async Task<IActionResult> DeleteKomentar(int komentarId)
    {
        return GetControllerResult(await _radioniceService.DeleteKomentar(komentarId));
    }

     [HttpPost("komentari/{komentarId}/like")]
    public async Task<IActionResult> LikeRadionicaKomentar(int komentarId)
    {
        return GetControllerResult(await _radioniceService.LikeRadionicaKomentar(komentarId));
    }

    [HttpPost("komentari/{komentarId}/dislike")]
    public async Task<IActionResult> DislikeRadionicaKomentar(int komentarId)
    {
        return GetControllerResult(await _radioniceService.DislikeRadionicaKomentar(komentarId));
    }

    [HttpDelete("komentari/{komentarId}/reakcija")]
    public async Task<IActionResult> ClearKomentarReaction(int komentarId)
    {
        return GetControllerResult(await _radioniceService.ClearKomentarReaction(komentarId));
    }

    [HttpPost("{radionicaId}/ocjena")]
    public async Task<IActionResult> CreateOcjena([FromBody] double ocjena, int radionicaId)
    {
        return GetControllerResult(await _radioniceService.CreateOcjena(radionicaId, ocjena));
    }

    [HttpDelete("{id}/ocjena")]
    public async Task<IActionResult> DeleteOcjena(int radionicaId)
    {
        return GetControllerResult(await _radioniceService.DeleteOcjena(radionicaId));
    }
    
}