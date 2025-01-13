using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachABit.Service.Services.Uloge;

namespace TeachABit.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UlogeController(IUlogeService rolesService) : BaseController
    {
        private readonly IUlogeService _rolesService = rolesService;

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllUloge()
        {
            return GetControllerResult(await _rolesService.GetAllUloge());
        }
    }
}
