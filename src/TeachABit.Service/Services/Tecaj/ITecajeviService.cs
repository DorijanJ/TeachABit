using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Tecaj;

namespace TeachABit.Service.Services.Tecaj;

public interface ITecajeviService
{
        Task<ServiceResult<List<TecajDto>>> GetTecajList();
        Task<ServiceResult<TecajDto>> GetTecaj(int id);
        Task<ServiceResult<TecajDto>> CreateTecaj(TecajDto tecaj);
        //Task<ServiceResult<TecajDto>> UpdateTecaj(TecajDto Tecaj);
        Task<ServiceResult> DeleteTecaj(int id);
}