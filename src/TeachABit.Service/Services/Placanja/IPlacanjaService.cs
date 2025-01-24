using TeachABit.Model.DTOs.Placanja;
using TeachABit.Model.DTOs.Result;

namespace TeachABit.Service.Services.Placanja
{
    public interface IPlacanjaService
    {
        public Task<ServiceResult<PlacanjeLinkDto>> CreateTecajCheckoutSession(TecajPlacanjeRequestDto requestDto);
        public Task<ServiceResult> CreateTecajPlacanje(string korisnikId, int tecajId);

        public Task<ServiceResult<PlacanjeLinkDto>> CreateRadionicaCheckoutSession(
            RadionicaPlacanjeRequestDto requestDto);

        public Task<ServiceResult> CreateRadionicaPlacanje(string korisnikId, int radionicaId);

    }
}
