using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using TeachABit.Model.DTOs.Placanja;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Service.Services.Placanja;
using TeachABit.Service.Util.Stripe;

namespace TeachABit.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacanjaController(IOptions<StripeSettings> stripeSettings, IPlacanjaService placanjaService) : BaseController
    {
        private readonly StripeSettings _stripeSettings = stripeSettings.Value;
        private readonly IPlacanjaService _placanjaService = placanjaService;

        [HttpGet]
        public IActionResult Checkout()
        {
            return Ok();
        }

        [HttpPost("napravi-stripe-account")]
        public IActionResult CreateAccountLink()
        {
            var options = new AccountCreateOptions
            {
                Type = "express",
            };
            var service = new AccountService();
            var account = service.Create(options);

            var linkOptions = new AccountLinkCreateOptions
            {
                Account = account.Id,
                RefreshUrl = "https://your-app.com/reauth",
                ReturnUrl = "https://your-app.com/success",
                Type = "account_onboarding",
            };
            var linkService = new AccountLinkService();
            var accountLink = linkService.Create(linkOptions);

            return Ok(new ControllerResult()
            {
                Data = new PlacanjeLinkDto()
                {
                    Url = accountLink.Url,
                },
                Message = MessageDescriber.SuccessMessage()
            });
        }

        [HttpPost("stripe-webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = new StreamReader(HttpContext.Request.Body).ReadToEnd();
            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], "your_webhook_secret");

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
                var metadata = session?.Metadata;

                if (metadata != null)
                {
                    metadata.TryGetValue("korisnikId", out string? korisnikId);
                    metadata.TryGetValue("tecajId", out string? tecajId);

                    if (korisnikId != null && tecajId != null)
                        await _placanjaService.CreateTecajPlacanje(korisnikId, int.Parse(tecajId));
                }
            }

            return Ok();
        }

        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] TecajPlacanjeRequestDto request)
        {
            return GetControllerResult(await _placanjaService.CreateTecajCheckoutSession(request));
        }
    }
}
