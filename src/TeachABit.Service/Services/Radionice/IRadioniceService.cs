using TeachABit.Model.DTOs.Radionice;
using TeachABit.Model.DTOs.Result;

namespace TeachABit.Service.Services.Radionice;

public interface IRadioniceService
{
    public Task<ServiceResult<List<RadionicaDto>>> GetRadionicaList(string? search = null, string? vlasnikUsername = null);
    public Task<ServiceResult<RadionicaDto>> GetRadionica(int id);
    public Task<ServiceResult<RadionicaDto>> CreateRadionica(CreateOrUpdateRadionicaDto radionica);
    public Task<ServiceResult<RadionicaDto>> UpdateRadionica(CreateOrUpdateRadionicaDto updateRadionica);
    public Task<ServiceResult> DeleteRadionica(int id);
    public Task<ServiceResult<List<RadionicaKomentarDto>>> GetKomentarListRecursive(int id, int? nadKomentarId = null);
    public Task<ServiceResult> DeleteKomentar(int id);
    public Task<ServiceResult<RadionicaKomentarDto>> CreateKomentar(RadionicaKomentarDto komentar, int objavaId);
    public Task<ServiceResult<RadionicaKomentarDto>> UpdateKomentar(UpdateKomentarRadionicaDto updateKomentar);
    public Task<ServiceResult> LikeRadionicaKomentar(int id);
    public Task<ServiceResult> DislikeRadionicaKomentar(int id);
    public Task<ServiceResult> ClearKomentarReaction(int id);
}