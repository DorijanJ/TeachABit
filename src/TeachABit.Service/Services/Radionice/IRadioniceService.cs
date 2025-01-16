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
}