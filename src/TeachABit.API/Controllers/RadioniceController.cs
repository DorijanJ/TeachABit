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
    public async Task<IActionResult> GetRadionicaList()
    {
        return GetControllerResult(await _radioniceService.GetRadionicaList());
    }

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
}