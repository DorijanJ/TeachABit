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
        public Task<List<Tecaj>> GetTecajList(string? search = null, string? korisnikId = null);
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
        public Task<KomentarTecaj> CreateKomentarTecaj(KomentarTecaj komentar);
        public Task DeleteKomentarTecaj(int id, bool keepEntry = false);
        public Task<KomentarTecaj> UpdateKomentarTecaj(KomentarTecaj komentar);
        public Task<KomentarTecaj?> GetKomentarTecajById(int id);
        public Task<List<KomentarTecaj>> GetKomentarTecajListByTecajId(int id);
        public Task<List<KomentarTecaj>> GetPodKomentarTecajList(int objavaId, int? nadKomentarTecajId = null);
        public Task DeleteKomentarTecajReakcija(int komentarId, string korisnikId);
        public Task DeleteKomentarTecajReakcija(int id);
        public Task<KomentarTecajReakcija> CreateKomentarTecajReakcija(KomentarTecajReakcija komentarReakcija);
        public Task<KomentarTecajReakcija?> GetKomentarTecajReakcija(int komentarId, string korisnikId);
        public Task<KomentarTecaj?> GetKomentarTecajByIdWithTracking(int id);
        public Task<bool> HasPodkomentari(int komentarId);

        public  Task<List<Tecaj>> GetTecajListByFiltratingCijena(int maxCijena, int minCijena,
            string? korisnikId = null);
        public Task<List<Tecaj>> GetTecajListByFiltratingOcjena(int ocjena, string? korisnikId = null)
    }
}