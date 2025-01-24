using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Model.Enums;
using TeachABit.Service.Util.Token;

namespace TeachABit.API.Middleware
{
    public class NotStatus(KorisnikStatusEnum status) : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly KorisnikStatusEnum _status = status;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            var statusClaim = user.Claims.FirstOrDefault(c => c.Type == CustomClaimTypes.KorisnikStatus);
            if (statusClaim == null)
            {
                return;
            }

            if (!int.TryParse(statusClaim.Value, out var roleValue) || roleValue == (int)_status)
            {
                context.Result = new UnauthorizedObjectResult(new ControllerResult()
                {
                    Message = MessageDescriber.Unauthorized(),
                    RefreshUserInfo = new() { IsAuthenticated = false }
                });
            }
        }
    }
}