using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TeachABit.Model;
using TeachABit.Model.Models.Radionice;

namespace TeachABit.Repository.Repositories.Radionice;

public class RadioniceRepository(TeachABitContext context) : IRadioniceRepository
{
    private readonly TeachABitContext _context = context;

    public async Task<List<Radionica>> GetRadionicaList(string? search = null)
    {
        IQueryable<Radionica> query = _context.Radionice
            .Include(x => x.Vlasnik)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var lowerSearch = search.ToLower();
            query = query.Where(x => x.Naziv.ToLower().Contains(lowerSearch));
        }

        return await query.ToListAsync();
    }

    public async Task<Radionica?> GetRadionica(int id)
    {
        return await _context.Radionice
            .Include(x => x.Vlasnik)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<Radionica> CreateRadionica(Radionica radionica)
    {
        EntityEntry<Radionica> createdZadatak = await _context.Radionice.AddAsync(radionica);
        await _context.SaveChangesAsync();
        return createdZadatak.Entity;
    }
    public async Task<Radionica> UpdateRadionica(Radionica radionica)
    {
        await _context.SaveChangesAsync();
        return radionica;
    }
    public async Task DeleteRadionica(int id)
    {
        await _context.Radionice.Where(x => x.Id == id).ExecuteDeleteAsync();
    }
    
    
    public async Task<Radionica?> GetRadionicaByIdWithTracking(int id)
    {
        return await _context.Radionice.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
    
    
    public async Task<KomentarRadionica> CreateKomentar(KomentarRadionica komentar)
    {
        var createdKomentar = await _context.KomentarRadionica.AddAsync(komentar);
        await _context.SaveChangesAsync();
        return createdKomentar.Entity;
    }
    
    public async Task DeleteKomentar(int id, bool keepEntry = false)
    {
        if (keepEntry)
            await _context.KomentarRadionica.Where(x => x.Id == id).ExecuteUpdateAsync(x => x.SetProperty(p => p.IsDeleted, true));
        else
            await _context.KomentarRadionica.Where(x => x.Id == id).ExecuteDeleteAsync();
    }
    
    public async Task<KomentarRadionica?> GetKomentarById(int id)
    {
        return await _context.KomentarRadionica.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<List<KomentarRadionica>> GetKomentarListByObjavaId(int id)
    {
        return await _context.KomentarRadionica
            .Include(x => x.Vlasnik)
            .Include(x => x.Radionica)
            .Include(x => x.KomentarRadionicaReakcijaList)
            .Where(x => x.Radionica.Id == id)
            .OrderByDescending(x => x.CreatedDateTime)
            .ToListAsync();
    }
    
    public async Task<List<KomentarRadionica>> GetPodKomentarList(int radionicaId, int? nadKomentarId = null)
    {
        var komentari = await _context.KomentarRadionica
            .Include(c => c.Vlasnik)
            .Include(c => c.Radionica)
            .Include(x => x.KomentarRadionicaReakcijaList)
            .Where(c => c.RadionicaId == radionicaId && c.NadKomentarId == nadKomentarId)
            .OrderByDescending(c => c.CreatedDateTime)
            .ToListAsync();

        return komentari;
    }
    
    public async Task<KomentarRadionicaReakcija> CreateKomentarReakcija(KomentarRadionicaReakcija komentarRadionicaReakcija)
    {
        var createdKomentarReakcija = await _context.KomentarRadionicaReakcija.AddAsync(komentarRadionicaReakcija);
        await _context.SaveChangesAsync();
        return createdKomentarReakcija.Entity;
    }
    
    public async Task DeleteKomentarReakcija(int komentarId, string korisnikId)
    {
        await _context.KomentarRadionicaReakcija
            .Where(x => x.KorisnikId == korisnikId && x.KomentarId == komentarId)
            .ExecuteDeleteAsync();
    }
    
    public async Task DeleteKomentarReakcija(int id)
    {
        await _context.KomentarRadionicaReakcija
            .Where(x => x.Id == id)
            .ExecuteDeleteAsync();
    }
    
    public async Task<KomentarRadionicaReakcija?> GetKomentarRadionicaReakcija(int komentarId, string korisnikId)
    {
        return await _context.KomentarRadionicaReakcija.FirstOrDefaultAsync(x => x.KomentarId == komentarId && x.KorisnikId == korisnikId);
    }
    
    public async Task<bool> HasPodkomentari(int komentarId)
    {
        return await _context.KomentarRadionica.AnyAsync(x => x.NadKomentarId == komentarId);
    }
    
    public async Task<KomentarRadionica> UpdateKomentar(KomentarRadionica komentar)
    {
        await _context.SaveChangesAsync();
        return komentar;
    }
    
    public async Task<KomentarRadionica?> GetKomentarRadionicaByIdWithTracking(int id)
    {
        return await _context.KomentarRadionica.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
    
}