using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TeachABit.Model;
using TeachABit.Model.Models.Radionice;

namespace TeachABit.Repository.Repositories.Radionice;

public class RadioniceRepository(TeachABitContext context) : IRadioniceRepository
{
    private readonly TeachABitContext _context = context;

    public async Task<List<Radionica>> GetRadionicaList(string? search = null, string? trenutniKorisnikId = null, string? vlasnikId = null, double? minOcjena = null,
        double? maxOcjena = null, bool sortOrderAsc = true)

    {
        IQueryable<Radionica> query = _context.Radionice
            .Include(x => x.Vlasnik)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var lowerSearch = search.ToLower();
            query = query.Where(x => x.Naziv.ToLower().Contains(lowerSearch));
        }

        if (minOcjena != null || maxOcjena != null) query = query.Include(x => x.Ocjene);

        if (minOcjena != null) query = query.Where(x => x.Ocjene.Average(o => o.Ocjena) >= minOcjena);
        if (maxOcjena != null) query = query.Where(x => x.Ocjene.Average(o => o.Ocjena) <= maxOcjena);

        if (!string.IsNullOrEmpty(trenutniKorisnikId))
        {
            query = query
                .Include(x => x.RadionicaFavoriti
                    .Where(f => f.KorisnikId == trenutniKorisnikId))
                .Include(x => x.Ocjene
                    .Where(x => x.KorisnikId == trenutniKorisnikId));
        }

        if (sortOrderAsc) query = query.OrderBy(x => x.VrijemeRadionice);
        else query = query.OrderByDescending(x => x.VrijemeRadionice);

        if (!string.IsNullOrEmpty(vlasnikId)) query = query.Where(x => x.VlasnikId == vlasnikId);

        return await query.ToListAsync();
    }

    public async Task<Radionica?> GetRadionica(int id)
    {
        return await _context.Radionice
            .Include(x => x.Vlasnik)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Radionica?> GetRadionicaById(int id)
    {
        return await _context.Radionice
            .Include(x => x.Vlasnik)
            .Include(x => x.Komentari)
            .ThenInclude(x => x.Vlasnik)
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



    public async Task<RadionicaKomentar> CreateKomentar(RadionicaKomentar komentar)
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

    public async Task<RadionicaKomentar?> GetKomentarById(int id)
    {
        return await _context.KomentarRadionica.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<RadionicaKomentar>> GetKomentarListByObjavaId(int id)
    {
        return await _context.KomentarRadionica
            .Include(x => x.Vlasnik)
            .Include(x => x.Radionica)
            .Include(x => x.KomentarRadionicaReakcijaList)
            .Where(x => x.Radionica.Id == id)
            .OrderByDescending(x => x.CreatedDateTime)
            .ToListAsync();
    }

    public async Task<List<RadionicaKomentar>> GetPodKomentarList(int radionicaId, int? nadKomentarId = null)
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

    public async Task<RadionicaKomentar> UpdateKomentar(RadionicaKomentar komentar)
    {
        await _context.SaveChangesAsync();
        return komentar;
    }

    public async Task<RadionicaOcjena?> GetOcjena(int radionicaId, string korisnikId)
    {
        return await _context.RadionicaOcjene
            .FirstOrDefaultAsync(o => o.RadionicaId == radionicaId && o.KorisnikId == korisnikId);
    }

    public async Task<RadionicaOcjena> CreateOcjena(RadionicaOcjena ocjena)
    {
        var entry = await _context.RadionicaOcjene.AddAsync(ocjena);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<RadionicaOcjena> UpdateOcjena(RadionicaOcjena ocjena)
    {
        await _context.SaveChangesAsync();
        return ocjena;
    }

    public async Task DeleteOcjena(int radionicaId, string korisnikId)
    {
        await _context.RadionicaOcjene
            .Where(x => x.RadionicaId == radionicaId && x.KorisnikId == korisnikId)
            .ExecuteDeleteAsync();
    }

    public async Task<RadionicaOcjena?> GetRadionicaOcjenaWithTracking(int radionicaId, string korisnikId)
    {
        return await _context.RadionicaOcjene.AsTracking().FirstOrDefaultAsync(x => x.RadionicaId == radionicaId && x.KorisnikId == korisnikId);
    }

    public async Task<RadionicaOcjena> UpdateRadionicaOcjena(RadionicaOcjena ocjena)
    {
        await _context.SaveChangesAsync();
        return ocjena;
    }

    public async Task<RadionicaKomentar?> GetKomentarRadionicaByIdWithTracking(int id)
    {
        return await _context.KomentarRadionica.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Radionica>> GetAllRadioniceForCurrentUser(string username)
    {
        var query = _context.Radionice
            .Include(x => x.Vlasnik)
            .AsQueryable();
        if (!string.IsNullOrEmpty(username))
        {
            query = query.Where(a => a.Vlasnik.UserName == username);
        }

        return await query.ToListAsync();
    }
    public async Task<List<RadionicaFavorit>> GetAllRadioniceFavoritForCurrentUser(string username)
    {
        var query = _context.RadionicaFavorit
            .Include(x => x.Korisnik)
            .AsQueryable();
        if (!string.IsNullOrEmpty(username))
        {
            query = query.Where(a => a.Korisnik.UserName == username);
        }

        return await query.ToListAsync();
    }
    public async Task<bool> CheckIfRadionicaPlacen(string korisnikId, int radinicaId)
    {
        var placanje = await _context.RadionicaPlacanja.FirstOrDefaultAsync(x => x.KorisnikId == korisnikId && x.RadionicaId == radinicaId);
        return placanje != null;
    }

    public async Task<RadionicaPlacanje> CreateRadionicaPlacanje(RadionicaPlacanje radionicaPlacanje)
    {
        var created = await _context.RadionicaPlacanja.AddAsync(radionicaPlacanje);
        await _context.SaveChangesAsync();
        return created.Entity;
    }
}