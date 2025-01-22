using TeachABit.Model.Models.Radionice;

namespace TeachABit.Repository.Repositories.Radionice;

public interface IRadioniceRepository
{
    Task<List<Radionica>> GetRadionicaList(string? search = null);
    Task<Radionica?> GetRadionica(int id);
    Task<Radionica> CreateRadionica(Radionica radionica);
    Task<Radionica> UpdateRadionica(Radionica radionica);
    Task DeleteRadionica(int id);
    Task<Radionica?> GetRadionicaByIdWithTracking(int id);
    
    public Task<KomentarRadionica> CreateKomentar(KomentarRadionica komentar);
    public Task<KomentarRadionica?> GetKomentarById(int id);
    public Task<bool> HasPodkomentari(int komentarId);
    public Task DeleteKomentar(int id, bool keepEntry = false);
    public Task<List<KomentarRadionica>> GetPodKomentarList(int objavaId, int? nadKomentarId = null);
    public Task<KomentarRadionica?> GetKomentarRadionicaByIdWithTracking(int id);
    public Task<KomentarRadionica> UpdateKomentar(KomentarRadionica komentar);
    
    Task<RadionicaOcjena?> GetOcjena(int radionicaId, string korisnikId);
    Task<RadionicaOcjena> CreateOcjena(RadionicaOcjena ocjena);
    Task<RadionicaOcjena> UpdateOcjena(RadionicaOcjena ocjena);
    Task DeleteOcjena(int radionicaId, string korisnikId);


}