using Microsoft.Extensions.Configuration;
using Stripe.Checkout;
using TeachABit.Model.DTOs.Placanja;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Model.Models.Radionice;
using TeachABit.Model.Models.Tecajevi;
using TeachABit.Repository.Repositories.Radionice;
using TeachABit.Repository.Repositories.Tecajevi;
using TeachABit.Service.Services.Authorization;

namespace TeachABit.Service.Services.Placanja
{
    public class PlacanjaService(IConfiguration configuration, ITecajeviRepository tecajeviRepository, IRadioniceRepository radioniceRepository, IAuthorizationService authorizationService) : IPlacanjaService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IAuthorizationService _authorizationService = authorizationService;
        private readonly ITecajeviRepository _tecajeviRepository = tecajeviRepository;
        private readonly IRadioniceRepository _radioniceRepository = radioniceRepository;

        public async Task<ServiceResult<PlacanjeLinkDto>> CreateTecajCheckoutSession(TecajPlacanjeRequestDto requestDto)
        {
            var domain = _configuration["ClientUrl"];

            if (domain == null) return ServiceResult.Failure(MessageDescriber.MissingConfiguration());

            var tecaj = await _tecajeviRepository.GetTecaj(requestDto.TecajId);

            if (tecaj == null || tecaj.Cijena == null) return ServiceResult.Failure(MessageDescriber.ItemNotFound());

            var korisnik = _authorizationService.GetKorisnik();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes =
            [
                "card",
            ],
                LineItems =
            [
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "eur",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = "Tečaj: " + tecaj.Naziv,
                        },
                        UnitAmount = (long)(tecaj.Cijena * 100)
                    },
                    Quantity = 1,
                },
            ],
                Mode = "payment",
                SuccessUrl = $"{domain}/success?session_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{domain}/cancel",
                Metadata = new Dictionary<string, string>
                {
                    { "tecajId", tecaj.Id.ToString() },
                    { "korisnikId",  korisnik.Id}
                }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            PlacanjeLinkDto sessionId = new()
            {
                Url = session.Id
            };


            return ServiceResult.Success(sessionId);
        }
        public async Task<ServiceResult<PlacanjeLinkDto>> CreateRadionicaCheckoutSession(RadionicaPlacanjeRequestDto requestDto)
        {
            var domain = _configuration["ClientUrl"];

            if (domain == null) return ServiceResult.Failure(MessageDescriber.MissingConfiguration());

            var radionica = await _radioniceRepository.GetRadionica(requestDto.RadionicaId);

            if (radionica == null || radionica.Cijena == null) return ServiceResult.Failure(MessageDescriber.ItemNotFound());

            var korisnik = _authorizationService.GetKorisnik();
            
            int brojPrijavljenih = radionica.Placanja.Count;

            if (radionica.MaksimalniKapacitet.HasValue)
            {
                if (brojPrijavljenih >= radionica.MaksimalniKapacitet.Value)
                {
                    return ServiceResult.Failure();
                }
            }


            var options = new SessionCreateOptions
            {
                PaymentMethodTypes =
                [
                    "card",
                ],
                LineItems =
                [
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "eur",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Radionica: " + radionica.Naziv,
                            },
                            UnitAmount = (long)(radionica.Cijena * 100)
                        },
                        Quantity = 1,
                    },
                ],
                Mode = "payment",
                SuccessUrl = $"{domain}/success?session_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{domain}/cancel",
                Metadata = new Dictionary<string, string>
                {
                    { "radionicaId", radionica.Id.ToString() },
                    { "korisnikId",  korisnik.Id}
                }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            PlacanjeLinkDto sessionId = new()
            {
                Url = session.Id
            };


            return ServiceResult.Success(sessionId);
        }

        public async Task<ServiceResult> CreateTecajPlacanje(string korisnikId, int tecajId)
        {
            var tecaj = await _tecajeviRepository.GetTecaj(tecajId);

            if (tecaj == null || !tecaj.Cijena.HasValue) return ServiceResult.Failure(MessageDescriber.ItemNotFound());

            TecajPlacanje tecajPlacanje = new() { KorisnikId = korisnikId, TecajId = tecajId, VrijemePlacanja = DateTime.UtcNow, PoCijeni = tecaj.Cijena.Value };

            await _tecajeviRepository.CreateTecajPlacanje(tecajPlacanje);

            return ServiceResult.Success();
        }
        public async Task<ServiceResult> CreateRadionicaPlacanje(string korisnikId, int radionicaId)
        {
            var radionica = await _radioniceRepository.GetRadionica(radionicaId);

            if (radionica == null || !radionica.Cijena.HasValue) return ServiceResult.Failure(MessageDescriber.ItemNotFound());

            RadionicaPlacanje radionicaPlacanje = new() { KorisnikId = korisnikId, RadionicaId = radionicaId, VrijemePlacanja = DateTime.UtcNow, PoCijeni = radionica.Cijena.Value };

            await _radioniceRepository.CreateRadionicaPlacanje(radionicaPlacanje);

            return ServiceResult.Success();
        }
    }
}
