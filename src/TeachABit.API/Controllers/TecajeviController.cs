using Microsoft.AspNetCore.Mvc;
using TeachABit.Service.Services.Tecaj;
using TeachABit.Model.DTOs.Result;
namespace TeachABit.API.Controllers;
using TeachABit.Model.DTOs.Tecaj;
[ApiController]
[Microsoft.AspNetCore.Components.Route("api/[controller]")]
public class TecajeviController(ITecajeviService tecajeviService) : BaseController
{
    private readonly ITecajeviService _tecajeviService = tecajeviService;

    [HttpGet]
    public async Task<IActionResult> GetTecajList()
    {
        return GetControllerResult(await _tecajeviService.GetTecajList());
    }

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