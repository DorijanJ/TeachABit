using TeachABit.Model.Models.Tecajevi;

namespace TeachABit.Repository.Repositories.Tecajevi
{
    public interface ITecajeviRepository
    {
        //Task<List<Tecaj>> GetTecajList();
        Task<Tecaj?> GetTecaj(int id);
        Task<Tecaj> CreateTecaj(Tecaj tecaj);
        Task<Tecaj> UpdateTecaj(Tecaj tecaj);
        Task DeleteTecaj(int id);
        Task<List<Tecaj>> GetTecajList(string? search = null);
        Task<Tecaj?> GetTecajByIdWithTracking(int id);
        public Task<Lekcija?> GetLekcijaByIdWithTracking(int id);
        public Task<Lekcija> CreateLekcija(Lekcija lekcija);
        public Task DeleteLekcija(int id, bool keepEntry = false);
        public Task<Lekcija> UpdateLekcija(Lekcija lekcija);
        public Task<Lekcija?> GetLekcijaById(int id);
        public Task<List<Lekcija>> GetLekcijaListByTecajId(int id);
    }
}