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
        if (string.IsNullOrWhiteSpace(search))
        {
            return await _context.Radionice.ToListAsync();
        }

        return await _context.Radionice
            .Where(r => r.Naziv.ToLower().Contains(search))
            .ToListAsync();
    }

    public async Task<Radionica?> GetRadionica(int id)
    {
        return await _context.Radionice.FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<Radionica> CreateRadionica(Radionica radionica)
    {
        EntityEntry<Radionica> createdZadatak = await _context.Radionice.AddAsync(radionica);
        await _context.SaveChangesAsync();
        return createdZadatak.Entity;
    }
    /*public async Task<Radionica> UpdateRadionica(Radionica radionica)
    {
        // Moram provjeriti najbolji način implementacije za update.
        ...
    }*/
    public async Task DeleteRadionica(int id)
    {
        await _context.Radionice.Where(x => x.Id == id).ExecuteDeleteAsync();
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
            .Where(x => x.Radionica.Id == id)
            .OrderByDescending(x => x.CreatedDateTime)
            .ToListAsync();
    }
    
    public async Task<List<KomentarRadionica>> GetPodKomentarList(int radionicaId, int? nadKomentarId = null)
    {
        var komentari = await _context.KomentarRadionica
            .Include(c => c.Vlasnik)
            .Include(c => c.Radionica)
            .Where(c => c.RadionicaId == radionicaId && c.NadKomentarId == nadKomentarId)
            .OrderByDescending(c => c.CreatedDateTime)
            .ToListAsync();

        return komentari;
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