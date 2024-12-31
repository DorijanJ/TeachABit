using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> CreateObjava(ObjavaDto objava)
        {
            return GetControllerResult(await _objaveService.CreateObjava(objava));
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
            return GetControllerResult(await _objaveService.GetKomentarListRecursive(id));
        }

        [HttpPost("{id}/komentari")]
        public async Task<IActionResult> CreateKomentar([FromBody] KomentarDto komentar, int id)
        {
            return GetControllerResult(await _objaveService.CreateKomentar(komentar, id));
        }

        [HttpPost("{id}/like")]
        public async Task<IActionResult> LikeObjava(int id)
        {
            return GetControllerResult(await _objaveService.LikeObjava(id));
        }

        [HttpPost("{objavaId}/komentari/{komentarId}/like")]
        public async Task<IActionResult> Like(int objavaId, int komentarId)
        {
            return GetControllerResult(await _objaveService.LikeKomentar(komentarId));
        }

        [HttpPost("{id}/dislike")]
        public async Task<IActionResult> DislikeObjava(int id)
        {
            return GetControllerResult(await _objaveService.DislikeObjava(id));
        }

        [HttpPost("{objavaId}/komentari/{komentarId}/dislike")]
        public async Task<IActionResult> DislikeKomentar(int objavaId, int komentarId)
        {
            return GetControllerResult(await _objaveService.DislikeKomentar(komentarId));
        }

        [HttpDelete("{id}/reakcija")]
        public async Task<IActionResult> ClearObjavaReaction(int id)
        {
            return GetControllerResult(await _objaveService.ClearObjavaReaction(id));
        }

        [HttpDelete("{objavaId}/komentari/{komentarId}/reakcija")]
        public async Task<IActionResult> ClearKomentarReaction(string objavaId, int komentarId)
        {
            return GetControllerResult(await _objaveService.ClearKomentarReaction(komentarId));
        }


    }
}
