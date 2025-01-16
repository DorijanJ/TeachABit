using TeachABit.Model.DTOs.Placanja;
using TeachABit.Model.DTOs.Result;

namespace TeachABit.Service.Services.Placanja
{
    public interface IPlacanjaService
    {
        public Task<ServiceResult<PlacanjeLinkDto>> CreateTecajCheckoutSession(TecajPlacanjeRequestDto requestDto);
        public Task<ServiceResult> CreateTecajPlacanje(string korisnikId, int tecajId);
    }
}
