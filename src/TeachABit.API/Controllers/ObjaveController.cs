﻿using Microsoft.AspNetCore.Authorization;
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

        [ModelStateFilter]
        [HttpPost]
        public async Task<IActionResult> CreateObjava(ObjavaDto objava)
        {
            return GetControllerResult(await _objaveService.CreateObjava(objava));
        }

        [HttpPut]
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

        [HttpPost("komentari/{komentarId}/like")]
        public async Task<IActionResult> Like(int komentarId)
        {
            return GetControllerResult(await _objaveService.LikeKomentar(komentarId));
        }

        [HttpPost("{id}/dislike")]
        public async Task<IActionResult> DislikeObjava(int id)
        {
            return GetControllerResult(await _objaveService.DislikeObjava(id));
        }

        [HttpPost("komentari/{komentarId}/dislike")]
        public async Task<IActionResult> DislikeKomentar(int komentarId)
        {
            return GetControllerResult(await _objaveService.DislikeKomentar(komentarId));
        }

        [HttpDelete("{id}/reakcija")]
        public async Task<IActionResult> ClearObjavaReaction(int id)
        {
            return GetControllerResult(await _objaveService.ClearObjavaReaction(id));
        }

        [HttpDelete("komentari/{komentarId}/reakcija")]
        public async Task<IActionResult> ClearKomentarReaction(int komentarId)
        {
            return GetControllerResult(await _objaveService.ClearKomentarReaction(komentarId));
        }

        [HttpPut("komentari")]
        public async Task<IActionResult> UpdateKomentar(UpdateKomentarDto updateKomentar)
        {
            return GetControllerResult(await _objaveService.UpdateKomentar(updateKomentar));
        }

        [HttpDelete("komentari/{komentarId}")]
        public async Task<IActionResult> DeleteKomentar(int komentarId)
        {
            return GetControllerResult(await _objaveService.DeleteKomentar(komentarId));
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
