using TeachABit.Model.Models.Radionice;

namespace TeachABit.Repository.Repositories.Radionice;

public interface IRadioniceRepository
{

    Task<List<Radionica>> GetRadionicaList(string? search = null, string? trenutniKorisnikId = null, string? vlasnikId = null, decimal? minCijena = null, decimal? maxCijena = null);
    Task<Radionica?> GetRadionica(int id);
    public Task<Radionica?> GetRadionicaById(int id);
    Task<Radionica> CreateRadionica(Radionica radionica);
    Task<Radionica> UpdateRadionica(Radionica radionica);
    Task DeleteRadionica(int id);
    Task<Radionica?> GetRadionicaByIdWithTracking(int id);

    Task<RadionicaOcjena> CreateOcjena(RadionicaOcjena ocjena);
    Task DeleteOcjena(int radionicaId, string korisnikId);
    Task<RadionicaOcjena?> GetRadionicaOcjenaWithTracking(int radionicaId, string korisnikId);
    Task<RadionicaOcjena> UpdateRadionicaOcjena(RadionicaOcjena ocjena);
    public Task<RadionicaKomentar> CreateKomentar(RadionicaKomentar komentar);
    public Task<RadionicaKomentar?> GetKomentarById(int id);
    public Task<bool> HasPodkomentari(int komentarId);
    public Task DeleteKomentar(int id, bool keepEntry = false);
    public Task<List<RadionicaKomentar>> GetPodKomentarList(int objavaId, int? nadKomentarId = null);
    public Task<KomentarRadionicaReakcija> CreateKomentarReakcija(KomentarRadionicaReakcija komentarRadionicaReakcija);
    public Task DeleteKomentarReakcija(int komentarId, string korisnikId);
    public Task DeleteKomentarReakcija(int id);
    public Task<KomentarRadionicaReakcija?> GetKomentarRadionicaReakcija(int komentarId, string korisnikId);
    public Task<RadionicaKomentar?> GetKomentarRadionicaByIdWithTracking(int id);
    public Task<RadionicaKomentar> UpdateKomentar(RadionicaKomentar komentar);
    public Task<List<RadionicaFavorit>> GetAllRadioniceFavoritForCurrentUser(string id);
}