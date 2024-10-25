using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeachABit.Model.DTOs.Authentication;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Service.Services.Authentication;
using IAuthorizationService = TeachABit.Service.Services.Authorization.IAuthorizationService;

namespace TeachABit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAuthenticationService authenticationService, IAuthorizationService authorizationService) : BaseController
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;
        private readonly IAuthorizationService _authorizationService = authorizationService;

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginAttemptDTO loginAttempt)
        {
            MessageResponse? modelStateErorr = GetModelStateError(MessageTypes.AuthenticationError);
            if (modelStateErorr != null)
                return GetControllerResult(modelStateErorr);

            return GetControllerResult(await _authenticationService.Login(loginAttempt));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterAttemptDTO registerAttempt)
        {
            MessageResponse? modelStateError = GetModelStateError(MessageTypes.AuthenticationError);
            if (modelStateError != null)
                return GetControllerResult(modelStateError);

            return GetControllerResult(await _authenticationService.Register(registerAttempt));
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return GetControllerResult(_authenticationService.Logout());
        }

        [HttpGet]
        public IActionResult GetCurrentUser()
        {
            return GetControllerResult(_authorizationService.GetUser());
        }
    }
}
