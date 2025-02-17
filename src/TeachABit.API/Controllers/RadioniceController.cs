using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachABit.API.Middleware;
using TeachABit.Model.DTOs.Placanja;
using TeachABit.Model.DTOs.Radionice;
using TeachABit.Service.Services.Placanja;
using TeachABit.Service.Services.Radionice;

namespace TeachABit.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RadioniceController(IRadioniceService radioniceService, IPlacanjaService placanjaService) : BaseController
{
    private readonly IRadioniceService _radioniceService = radioniceService;
    private readonly IPlacanjaService _placanjaService = placanjaService;

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetRadionicaList(string? search = null, string? vlasnikUsername = null, double? minOcjena = null,
        double? maxOcjena = null, decimal? minCijena = null, decimal? maxCijena = null, bool sortOrderAsc = true, bool samoNadolazece = true)
    {
        var result = await _radioniceService.GetRadionicaList(search, vlasnikUsername, minOcjena, maxOcjena, minCijena, maxCijena, sortOrderAsc, samoNadolazece);
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
    [HttpPost("create-checkout-session")]
    public async Task<IActionResult> CreateCheckoutSession([FromBody] RadionicaPlacanjeRequestDto request)
    {
        return GetControllerResult(await _placanjaService.CreateRadionicaCheckoutSession(request));
    }
    [HttpPost("{radionicaId}/obavijest")]
    public async Task<IActionResult> SendObavijest(int radionicaId, [FromBody] ObavijestDto obavijestDto)
    {
        obavijestDto.RadionicaId = radionicaId;
        return GetControllerResult(await _radioniceService.SendObavijest(obavijestDto));
    }
    [HttpGet("favoriti")]
    public async Task<IActionResult> GetAllRadioniceFavoritForCurrentUser()
    {
        return GetControllerResult(await _radioniceService.GetAllRadioniceFavoritForCurrentUser());
    }
    
    [HttpPost("{favoritRadionicaId}/favorit")]
    public async Task<IActionResult> AddFavoritRadionica(int favoritRadionicaId)
    {
        return GetControllerResult(await _radioniceService.AddFavoritRadionica(favoritRadionicaId));
    }

    [HttpDelete("{favoritRadionicaId}/favorit")]
    public async Task<IActionResult> RemoveFavoritRadionica(int favoritRadionicaId)
    {
        return GetControllerResult(await _radioniceService.RemoveFavoritRadionica(favoritRadionicaId));
    }
    
}