using TeachABit.Model.DTOs.Radionice;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.Models.Radionice;

namespace TeachABit.Service.Services.Radionice;

public interface IRadioniceService
{
    public Task<ServiceResult<List<RadionicaDto>>> GetRadionicaList(string? search = null);
    public Task<ServiceResult<RadionicaDto>> GetRadionica(int id);
    public Task<ServiceResult<RadionicaDto>> CreateRadionica(RadionicaDto radionica);
    public Task<ServiceResult<RadionicaDto>> UpdateRadionica(UpdateRadionicaDto updateRadionica);
    public Task<ServiceResult> DeleteRadionica(int id);
    public Task<ServiceResult<List<KomentarRadionicaDto>>> GetKomentarListRecursive(int id, int? nadKomentarId = null);
    public Task<ServiceResult> DeleteKomentar(int id);
    public Task<ServiceResult<KomentarRadionicaDto>> CreateKomentar(KomentarRadionicaDto komentar, int objavaId);
    public Task<ServiceResult<KomentarRadionicaDto>> UpdateKomentar(UpdateKomentarRadionicaDto updateKomentar);

    Task<ServiceResult<RadionicaOcjena?>> GetOcjena(int radionicaId, string korisnikId);
    Task<ServiceResult<RadionicaOcjena>> CreateOcjena(int radionicaId, string korisnikId, double ocjena);
    Task<ServiceResult<RadionicaOcjena>> UpdateOcjena(int radionicaId, string korisnikId, double ocjena);
    Task<ServiceResult> DeleteOcjena(int radionicaId, string korisnikId);
}