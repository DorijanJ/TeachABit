using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Tecajevi;

namespace TeachABit.Service.Services.Tecajevi
{
    public interface ITecajeviService
    {
        public Task<ServiceResult<TecajDto>> GetTecaj(int id);
        public Task<ServiceResult<TecajDto>> CreateTecaj(CreateOrUpdateTecajDto tecaj);
        //Task<ServiceResult<TecajDto>> UpdateTecaj(TecajDto Tecaj);
        Task<ServiceResult<TecajDto>> UpdateTecaj(CreateOrUpdateTecajDto updateTecaj);
        public Task<ServiceResult> DeleteTecaj(int id);
        public Task<ServiceResult<List<TecajDto>>> GetTecajList(string? search = null);
        public Task<ServiceResult<LekcijaDto>> CreateLekcija(LekcijaDto lekcijaDto, int id);
        public Task<ServiceResult> DeleteLekcija(int id);
        public Task<ServiceResult<LekcijaDto>> UpdateLekcija(UpdatedLekcijaDto updateLekcija);
        public Task<ServiceResult<List<LekcijaDto>>> GetLekcijaList(string? search = null);
        public Task<ServiceResult<KomentarTecajDto>> CreateKomentarTecaj(KomentarTecajDto KomentarTecaj, int objavaId);
        public Task<ServiceResult<List<KomentarTecajDto>>> GetKomentarTecajListRecursive(int id, int? nadKomentarTecajId = null);
        public Task<ServiceResult> DeleteKomentarTecaj(int id);
        public Task<ServiceResult> LikeKomentarTecaj(int id);
        public Task<ServiceResult> DislikeKomentarTecaj(int id);
        public Task<ServiceResult> ClearKomentarTecajReaction(int id);
        public Task<ServiceResult<KomentarTecajDto>> UpdateKomentarTecaj(UpdateKomentarTecajDto updateKomentarTecaj);
        public  Task<ServiceResult<List<TecajDto>>> GetAllTecajeviFavoritForCurrentUser(string username);
        public Task<ServiceResult<List<TecajDto>>> GetAllTecajeviForCurrentUser(string username);
    }
}