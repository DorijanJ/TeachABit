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
    public async Task<IActionResult> GetRadionicaList(string? search = null)
    {
        var result = await _radioniceService.GetRadionicaList(search);
        return GetControllerResult(result);
    }
    
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRadionica(int id)
    {
        return GetControllerResult(await _radioniceService.GetRadionica(id));
    }

    [ModelStateFilter]
    [HttpPost]
    public async Task<IActionResult> CreateRadionica(RadionicaDto radionica)
    {
        return GetControllerResult(await _radioniceService.CreateRadionica(radionica));
    }

    [ModelStateFilter]
    [HttpPut]
    public async Task<IActionResult> UpdateRadionica(UpdateRadionicaDto radionica)
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
    public async Task<IActionResult> CreateKomentar([FromBody] KomentarRadionicaDto komentar, int id)
    {
        return GetControllerResult(await _radioniceService.CreateKomentar(komentar, id));
    }
    
    [HttpPut("komentari")]
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
    [HttpGet("radionice/{username}")]
    public async Task<IActionResult> GetAllRadioniceForCurrentUser(string username)
    {
        return GetControllerResult(await _radioniceService.GetAllRadioniceForCurrentUser(username));
    }
    [HttpGet("radionicefavrit/{username}")]
    public async Task<IActionResult> GetAllRadioniceFavoritForCurrentUser(string username)
    {
        return GetControllerResult(await _radioniceService.GetAllRadionicefavoritForCurrentUser(username));
    }
}