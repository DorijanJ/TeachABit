using TeachABit.Model.DTOs.Radionice;
using TeachABit.Model.DTOs.Result;

namespace TeachABit.Service.Services.Radionice;

public interface IRadioniceService
{
    Task<ServiceResult<List<RadionicaDto>>> GetRadionicaList(string? search = null);
    Task<ServiceResult<RadionicaDto>> GetRadionica(int id);
    Task<ServiceResult<RadionicaDto>> CreateRadionica(RadionicaDto radionica);
    Task<ServiceResult> DeleteRadionica(int id);
    public Task<ServiceResult<List<KomentarRadionicaDto>>> GetKomentarListRecursive(int id, int? nadKomentarId = null);
    public Task<ServiceResult> DeleteKomentar(int id);
    public Task<ServiceResult<KomentarRadionicaDto>> CreateKomentar(KomentarRadionicaDto komentar, int objavaId);
    public Task<ServiceResult<KomentarRadionicaDto>> UpdateKomentar(UpdateKomentarRadionicaDto updateKomentar);

    
}