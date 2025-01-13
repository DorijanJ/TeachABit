using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachABit.Service.Services.Uloge;

namespace TeachABit.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController(IRolesService rolesService) : BaseController
    {
        private readonly IRolesService _rolesService = rolesService;

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            return GetControllerResult(await _rolesService.GetAllRoles());
        }
    }
}
