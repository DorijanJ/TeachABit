using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Tecajevi;
using TeachABit.Model.Models.Tecajevi;

namespace TeachABit.Service.Services.Tecajevi
{
    public interface ITecajeviService
    {
        //Task<ServiceResult<List<TecajDto>>> GetTecajList();
        Task<ServiceResult<TecajDto>> GetTecaj(int id);
        Task<ServiceResult<TecajDto>> CreateTecaj(TecajDto tecaj);
        //Task<ServiceResult<TecajDto>> UpdateTecaj(TecajDto Tecaj);
        Task<ServiceResult> DeleteTecaj(int id);
        Task<ServiceResult<List<TecajDto>>> GetTecajList(string search = null);
    }
}