using TeachABit.Model.Models.Tecajevi;

namespace TeachABit.Repository.Repositories.Tecajevi
{
    public interface ITecajeviRepository
    {
        //Task<List<Tecaj>> GetTecajList();
        public Task<Tecaj?> GetTecaj(int id);
        public Task<Tecaj> CreateTecaj(Tecaj tecaj);
        public Task<Tecaj> UpdateTecaj(Tecaj tecaj);
        public Task DeleteTecaj(int id);
        public Task<List<Tecaj>> GetTecajList(string? search = null);
        public Task<Tecaj?> GetTecajByIdWithTracking(int id);
        public Task<Lekcija?> GetLekcijaByIdWithTracking(int id);
        public Task<Lekcija> CreateLekcija(Lekcija lekcija);
        public Task DeleteLekcija(int id, bool keepEntry = false);
        public Task<Lekcija> UpdateLekcija(Lekcija lekcija);
        public Task<Lekcija?> GetLekcijaById(int id);
        public Task<List<Lekcija>> GetLekcijaListByTecajId(int id);
        public Task<bool> CheckIfTecajPlacen(string korisnikId, int tecajId);
        public Task<TecajPlacanje> CreateTecajPlacanje(TecajPlacanje tecajPlacanje);
        public Task<List<Lekcija>> GetLekcijaList(string? search = null);
    }
}