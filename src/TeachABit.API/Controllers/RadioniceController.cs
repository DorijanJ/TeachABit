using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachABit.Model.DTOs.Radionice;
using TeachABit.Service.Services.Radionice;

namespace TeachABit.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RadioniceController(IRadioniceService radioniceService) : BaseController
{
    private readonly IRadioniceService _radioniceService = radioniceService;

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

    [HttpPost]
    public async Task<IActionResult> CreateRadionica(RadionicaDto radionica)
    {
        return GetControllerResult(await _radioniceService.CreateRadionica(radionica));
    }

    /*[HttpPut]
    public async Task<IActionResult> UpdateZadatak(ZadatakDto zadatak)
    {
        return GetControllerResult(await _zadatciService.UpdateZadatak(zadatak));
    }*/

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
    
}