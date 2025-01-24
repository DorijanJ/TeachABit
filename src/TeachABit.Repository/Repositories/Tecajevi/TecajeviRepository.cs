using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TeachABit.Model;
using TeachABit.Model.Models.Tecajevi;

namespace TeachABit.Repository.Repositories.Tecajevi
{
    public class TecajeviRepository(TeachABitContext context) : ITecajeviRepository
    {
        private readonly TeachABitContext _context = context;

        public async Task<Tecaj?> GetTecaj(int id)
        {
            return await _context.Tecajevi
                .Include(x => x.Vlasnik)
                .Include(x => x.Lekcije)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Tecaj> UpdateTecaj(Tecaj tecaj)
        {
            await _context.SaveChangesAsync();
            return tecaj;
        }
        public async Task<Tecaj> CreateTecaj(Tecaj tecaj)
        {
            EntityEntry<Tecaj> createdTecaj = await _context.Tecajevi.AddAsync(tecaj);
            await _context.SaveChangesAsync();
            return createdTecaj.Entity;
        }
        public async Task DeleteTecaj(int id)
        {
            await _context.Tecajevi.Where(x => x.Id == id).ExecuteDeleteAsync();
        }
        public async Task<List<Tecaj>> GetTecajList(string? search = null, string? trenutniKorisnikId = null, string? vlasnikId = null, decimal? minCijena = null, decimal? maxCijena = null)
        {
            var query = _context.Tecajevi
                .Include(x => x.Vlasnik)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                string lowerSearch = search.ToLower();
                query = query.Where(t => t.Naziv.ToLower().Contains(lowerSearch));
            }

            if (minCijena != null && minCijena > 0) query = query.Where(x => x.Cijena >= minCijena);
            if (maxCijena != null) query = query.Where(x => x.Cijena <= maxCijena || x.Cijena == null);

            if (!string.IsNullOrEmpty(vlasnikId)) query = query.Where(x => x.VlasnikId == vlasnikId);

            if (!string.IsNullOrEmpty(trenutniKorisnikId))
            {
                query = query
                    .Include(x => x.KorisnikTecajFavoriti
                        .Where(x => x.KorisnikId == trenutniKorisnikId))
                    .Include(x => x.KorisnikTecajOcjene
                        .Where(x => x.KorisnikId == trenutniKorisnikId))
                    .Include(x => x.TecajPlacanja
                        .Where(t => t.KorisnikId == trenutniKorisnikId));
            }

            return await query.ToListAsync();
        }
        public async Task<List<Lekcija>> GetLekcijaList(string? search = null)
        {
            if (!string.IsNullOrEmpty(search))
            {
                string lowerSearch = search.ToLower();
                return await _context.Lekcije
                    .Where(t => t.Naziv.ToLower().Contains(lowerSearch))
                    .ToListAsync();
            }
            return await _context.Lekcije.ToListAsync();
        }
        public async Task<Tecaj?> GetTecajByIdWithTracking(int id)
        {
            return await _context.Tecajevi.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Lekcija?> GetLekcijaByIdWithTracking(int id)
        {
            return await _context.Lekcije.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Lekcija> CreateLekcija(Lekcija lekcija)
        {
            var createdLekcija = await _context.Lekcije.AddAsync(lekcija);
            await _context.SaveChangesAsync();
            return createdLekcija.Entity;
        }

        public async Task DeleteLekcija(int id, bool keepEntry = false)
        {
            await _context.Lekcije.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<Lekcija> UpdateLekcija(Lekcija lekcija)
        {
            await _context.SaveChangesAsync();
            return lekcija;
        }

        public async Task<Lekcija?> GetLekcijaById(int id)
        {
            return await _context.Lekcije.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Lekcija>> GetLekcijaListByTecajId(int id)
        {
            return await _context.Lekcije
                .Include(x => x.Tecaj)
                .ThenInclude(x => x.Vlasnik)
                .Where(x => x.Tecaj.Id == id)
                .OrderByDescending(x => x.CreatedDateTime)
                .ToListAsync();
        }

        public async Task<bool> CheckIfTecajPlacen(string korisnikId, int tecajId)
        {
            var placanje = await _context.TecajPlacanja.FirstOrDefaultAsync(x => x.KorisnikId == korisnikId && x.TecajId == tecajId);
            return placanje != null;
        }

        public async Task<TecajPlacanje> CreateTecajPlacanje(TecajPlacanje tecajPlacanje)
        {
            var created = await _context.TecajPlacanja.AddAsync(tecajPlacanje);
            await _context.SaveChangesAsync();
            return created.Entity;
        }

        public async Task<TecajKomentar> CreateKomentarTecaj(TecajKomentar komentarTecaj)
        {
            var createdKomentarTecaj = await _context.KomentariTecaj.AddAsync(komentarTecaj);
            await _context.SaveChangesAsync();
            return createdKomentarTecaj.Entity;
        }

        public async Task DeleteKomentarTecaj(int id, bool keepEntry = false)
        {
            if (keepEntry)
                await _context.KomentariTecaj.Where(x => x.Id == id).ExecuteUpdateAsync(x => x.SetProperty(p => p.IsDeleted, true));
            else
                await _context.KomentariTecaj.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<TecajKomentar> UpdateKomentarTecaj(TecajKomentar komentar)
        {
            await _context.SaveChangesAsync();
            return komentar;
        }

        public async Task<TecajKomentar?> GetKomentarTecajById(int id)
        {
            return await _context.KomentariTecaj.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TecajKomentar>> GetKomentarTecajListByTecajId(int id)
        {
            return await _context.KomentariTecaj
                .Include(x => x.Vlasnik)
                .Include(x => x.Tecaj)
                .Include(x => x.KomentarReakcijaList)
                .Where(x => x.Tecaj.Id == id)
                .OrderByDescending(x => x.CreatedDateTime)
                .ToListAsync();
        }

        public async Task<List<TecajKomentar>> GetPodKomentarTecajList(int tecajId, int? nadKomentarTecajId = null)
        {
            var komentari = await _context.KomentariTecaj
                .Include(c => c.Vlasnik)
                .Include(c => c.Tecaj)
                .Include(c => c.KomentarReakcijaList)
                .Where(c => c.TecajId == tecajId && c.NadKomentarId == nadKomentarTecajId)
                .OrderByDescending(c => c.CreatedDateTime)
                .ToListAsync();
            return komentari;
        }

        public async Task DeleteKomentarTecajReakcija(int komentarId, string korisnikId)
        {
            await _context.KomentarTecajReakcije
                .Where(x => x.KorisnikId == korisnikId && x.KomentarId == komentarId)
                .ExecuteDeleteAsync();
        }

        public async Task DeleteKomentarTecajReakcija(int id)
        {
            await _context.KomentarTecajReakcije
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync();
        }

        public async Task<KomentarTecajReakcija> CreateKomentarTecajReakcija(KomentarTecajReakcija komentarReakcija)
        {
            var createdKomentarReakcija = await _context.KomentarTecajReakcije.AddAsync(komentarReakcija);
            await _context.SaveChangesAsync();
            return createdKomentarReakcija.Entity;
        }

        public async Task<KomentarTecajReakcija?> GetKomentarTecajReakcija(int komentarId, string korisnikId)
        {
            return await _context.KomentarTecajReakcije.FirstOrDefaultAsync(x => x.KomentarId == komentarId && x.KorisnikId == korisnikId);

        }

        public async Task<TecajKomentar?> GetKomentarTecajByIdWithTracking(int id)
        {
            return await _context.KomentariTecaj.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> HasPodkomentari(int komentarId)
        {
            return await _context.KomentariTecaj.AnyAsync(x => x.NadKomentarId == komentarId);
        }
        public async Task<KorisnikTecajOcjena> CreateKorisnikTecajOcjena(KorisnikTecajOcjena ocjena)
        {
            EntityEntry<KorisnikTecajOcjena> createdOcjena = await _context.KorisnikTecajOcjene.AddAsync(ocjena);
            await _context.SaveChangesAsync();
            return createdOcjena.Entity;
        }
        public async Task DeleteKorisnikTecajOcjena(int tecajId, string korisnikId)
        {
            await _context.KorisnikTecajOcjene.Where(x => x.TecajId == tecajId && x.KorisnikId == korisnikId).ExecuteDeleteAsync();
        }

        public async Task<KorisnikTecajOcjena?> GetTecajOcjenaWithTracking(int tecajId, string korisnikId)
        {
            return await _context.KorisnikTecajOcjene.AsTracking().FirstOrDefaultAsync(x => x.TecajId == tecajId && x.KorisnikId == korisnikId);
        }

        public async Task<KorisnikTecajOcjena> UpdateTecajOcjena(KorisnikTecajOcjena ocjena)
        {
            await _context.SaveChangesAsync();
            return ocjena;
        }

        public async Task<TecajFavorit> AddFavoritTecaj(TecajFavorit favorit)
        {
            EntityEntry<TecajFavorit> favoritt = await _context.TecajFavoriti.AddAsync(favorit);
            await _context.SaveChangesAsync();
            return favoritt.Entity;
        }

        public async Task<TecajFavorit?> GetFavoritTecajById(int id)
        {
            return await _context.TecajFavoriti.AsTracking().FirstOrDefaultAsync(x => x.Id == id);

        }
        public async Task RemoveFavoritTecaj(int favoritTecajId)
        {
            await _context.TecajFavoriti.Where(x=> x.Id==favoritTecajId).ExecuteDeleteAsync();
        }
        public async Task<List<TecajFavorit>> GetAllTecajeviFavoritForCurrentUser(string id)
        {
            var query = _context.TecajFavoriti
                .Include(x => x.Korisnik)
                .AsQueryable();
            if (!string.IsNullOrEmpty(id))
            {
                query = query.Where(a => a.KorisnikId == id);
            }

            return await query.ToListAsync();
        }
    }
}