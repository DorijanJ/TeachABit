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
}