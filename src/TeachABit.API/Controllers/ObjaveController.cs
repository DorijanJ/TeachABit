using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachABit.API.Middleware;
using TeachABit.Model.DTOs.Objave;
using TeachABit.Service.Services.Objave;

namespace TeachABit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjaveController(IObjaveService objaveService) : BaseController
    {
        private readonly IObjaveService _objaveService = objaveService;

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetObjavaList(string? search, string? username)
        {
            return GetControllerResult(await _objaveService.GetObjavaList(search, username));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetObjavaById(int id)
        {
            return GetControllerResult(await _objaveService.GetObjavaById(id));
        }

        [HttpPost]
        [ModelStateFilter]
        public async Task<IActionResult> CreateObjava(ObjavaDto objava)
        {
            return GetControllerResult(await _objaveService.CreateObjava(objava));
        }

        [HttpPut]
        [ModelStateFilter]
        public async Task<IActionResult> UpdateObjava(UpdateObjavaDto updateObjava)
        {
            return GetControllerResult(await _objaveService.UpdateObjava(updateObjava));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteObjava(int id)
        {
            return GetControllerResult(await _objaveService.DeleteObjava(id));
        }

        [AllowAnonymous]
        [HttpGet("{id}/komentari")]
        public async Task<IActionResult> GetKomentarListByObjavaId(int id)
        {
            return GetControllerResult(await _objaveService.GetObjavaKomentarListRecursive(id));
        }

        [HttpPost("{id}/komentari")]
        [ModelStateFilter]
        public async Task<IActionResult> CreateKomentar([FromBody] ObjavaKomentarDto komentar, int id)
        {
            return GetControllerResult(await _objaveService.CreateObjavaKomentar(komentar, id));
        }

        [HttpPost("{id}/like")]
        public async Task<IActionResult> LikeObjava(int id)
        {
            return GetControllerResult(await _objaveService.LikeObjava(id));
        }

        [HttpPost("komentari/{komentarId}/like")]
        public async Task<IActionResult> LikeObjavaKomentar(int komentarId)
        {
            return GetControllerResult(await _objaveService.LikeObjavaKomentar(komentarId));
        }

        [HttpPost("{id}/dislike")]
        public async Task<IActionResult> DislikeObjava(int id)
        {
            return GetControllerResult(await _objaveService.DislikeObjava(id));
        }

        [HttpPost("komentari/{komentarId}/dislike")]
        public async Task<IActionResult> DislikeObjavaKomentar(int komentarId)
        {
            return GetControllerResult(await _objaveService.DislikeObjavaKomentar(komentarId));
        }

        [HttpDelete("{id}/reakcija")]
        public async Task<IActionResult> ClearObjavaReaction(int id)
        {
            return GetControllerResult(await _objaveService.ClearObjavaKomentarReaction(id));
        }

        [HttpDelete("komentari/{komentarId}/reakcija")]
        public async Task<IActionResult> ClearKomentarReaction(int komentarId)
        {
            return GetControllerResult(await _objaveService.ClearObjavaKomentarReaction(komentarId));
        }

        [HttpPut("komentari")]
        [ModelStateFilter]
        public async Task<IActionResult> UpdateKomentar(UpdateObjavaKomentarDto updateKomentar)
        {
            return GetControllerResult(await _objaveService.UpdateObjavaKomentar(updateKomentar));
        }

        [HttpDelete("komentari/{komentarId}")]
        public async Task<IActionResult> DeleteKomentar(int komentarId)
        {
            return GetControllerResult(await _objaveService.DeleteObjavaKomentar(komentarId));
        }

        [HttpPost("komentari/{komentarId}/tocan")]
        public async Task<IActionResult> OznaciTocanKomentar(int komentarId)
        {
            return GetControllerResult(await _objaveService.OznaciKaoTocan(komentarId));
        }

        [HttpDelete("komentari/{komentarId}/tocan")]
        public async Task<IActionResult> ClearTocanKomentar(int komentarId)
        {
            return GetControllerResult(await _objaveService.ClearTocanKomentar(komentarId));
        }
    }
}
