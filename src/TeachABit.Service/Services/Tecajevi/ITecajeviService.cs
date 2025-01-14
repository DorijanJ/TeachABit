using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Tecajevi;

namespace TeachABit.Service.Services.Tecajevi
{
    public interface ITecajeviService
    {
        //Task<ServiceResult<List<TecajDto>>> GetTecajList();
        public  Task<ServiceResult<TecajDto>> GetTecaj(int id);
        public Task<ServiceResult<TecajDto>> CreateTecaj(TecajDto tecaj);
        //Task<ServiceResult<TecajDto>> UpdateTecaj(TecajDto Tecaj);
        Task<ServiceResult> DeleteTecaj(int id);
        Task<ServiceResult<List<TecajDto>>> GetTecajList(string? search = null);
        Task<ServiceResult<TecajDto>> UpdateTecaj(UpdateTecajDto updateObjava);
        public Task<ServiceResult> DeleteTecaj(int id);
        public Task<ServiceResult<List<TecajDto>>> GetTecajList(string? search = null);
        public Task<ServiceResult<LekcijaDto>> CreateLekcija(LekcijaDto lekcijaDto, int id);
        public Task<ServiceResult> DeleteLekcija(int id);
        public Task<ServiceResult<LekcijaDto>> UpdateLekcija(UpdatedLekcijaDto updateLekcija);
    }
}