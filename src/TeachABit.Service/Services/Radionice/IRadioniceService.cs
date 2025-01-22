using TeachABit.Model.DTOs.Radionice;
using TeachABit.Model.DTOs.Result;

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
    public Task<ServiceResult> LikeRadionicaKomentar(int id);
    public Task<ServiceResult> DislikeRadionicaKomentar(int id);
    public Task<ServiceResult> ClearKomentarReaction(int id);
    public  Task<ServiceResult<List<RadionicaDto>>> GetAllRadionicefavoritForCurrentUser(string username);
    public  Task<ServiceResult<List<RadionicaDto>>> GetAllRadioniceForCurrentUser(string username);


}