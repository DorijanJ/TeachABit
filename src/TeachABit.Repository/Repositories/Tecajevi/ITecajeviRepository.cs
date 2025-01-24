using TeachABit.Model.Models.Tecajevi;

namespace TeachABit.Repository.Repositories.Tecajevi
{
    public interface ITecajeviRepository
    {
        public Task<Tecaj?> GetTecaj(int id);
        public Task<Tecaj> CreateTecaj(Tecaj tecaj);
        public Task<Tecaj> UpdateTecaj(Tecaj tecaj);
        public Task DeleteTecaj(int id);
        public Task<List<Tecaj>> GetTecajList(string? search = null, string? trenutniKorisnikId = null, string? vlasnikId = null, decimal? minCijena = null, decimal? maxCijena = null, int? minOcjena = null, int? maxOcjena = null, bool? vrmenski_najstarije=null);
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
        public Task<TecajKomentar> CreateKomentarTecaj(TecajKomentar komentar);
        public Task DeleteKomentarTecaj(int id, bool keepEntry = false);
        public Task<TecajKomentar> UpdateKomentarTecaj(TecajKomentar komentar);
        public Task<TecajKomentar?> GetKomentarTecajById(int id);
        public Task<List<TecajKomentar>> GetKomentarTecajListByTecajId(int id);
        public Task<List<TecajKomentar>> GetPodKomentarTecajList(int objavaId, int? nadKomentarTecajId = null);
        public Task DeleteKomentarTecajReakcija(int komentarId, string korisnikId);
        public Task DeleteKomentarTecajReakcija(int id);
        public Task<KomentarTecajReakcija> CreateKomentarTecajReakcija(KomentarTecajReakcija komentarReakcija);
        public Task<KomentarTecajReakcija?> GetKomentarTecajReakcija(int komentarId, string korisnikId);
        public Task<TecajKomentar?> GetKomentarTecajByIdWithTracking(int id);
        public Task<bool> HasPodkomentari(int komentarId);
        public Task<KorisnikTecajOcjena> CreateKorisnikTecajOcjena(KorisnikTecajOcjena ocjena);
        public Task DeleteKorisnikTecajOcjena(int tecajId, string korisnikId);
        public Task<KorisnikTecajOcjena?> GetTecajOcjenaWithTracking(int tecajId, string korisnikId);
        public Task<KorisnikTecajOcjena> UpdateTecajOcjena(KorisnikTecajOcjena ocjena);
        public Task<List<TecajFavorit>> GetAllTecajeviFavoritForCurrentUser(string username);
        public Task RemoveFavoritTecaj(int favoritTecajId);
        public Task<TecajFavorit?> AddFavoritTecaj(TecajFavorit favorit);
    }
}