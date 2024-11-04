﻿using Microsoft.AspNetCore.Mvc;
using TeachABit.Model.DTOs.Objave;
using TeachABit.Service.Services.Objave;

namespace TeachABit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjaveController(IObjaveService objaveService) : BaseController
    {
        private readonly IObjaveService _objaveService = objaveService;

        [HttpGet]
        public async Task<IActionResult> GetObjavaList()
        {
            return GetControllerResult(await _objaveService.GetObjavaList());
        }

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

    }
}