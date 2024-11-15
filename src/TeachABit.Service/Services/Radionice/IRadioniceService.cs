using TeachABit.Model.DTOs.Radionice;
using TeachABit.Model.DTOs.Result;

namespace TeachABit.Service.Services.Radionice;

public interface IRadioniceService
{
    Task<ServiceResult<List<RadionicaDto>>> GetRadionicaList(string? search = null);
    Task<ServiceResult<RadionicaDto>> GetRadionica(int id);
    Task<ServiceResult<RadionicaDto>> CreateRadionica(RadionicaDto radionica);
    Task<ServiceResult> DeleteRadionica(int id);
}