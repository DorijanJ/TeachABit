using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TeachABit.Model;
using TeachABit.Model.Models.Tecajevi;

namespace TeachABit.Repository.Repositories.Tecajevi
{
    public class TecajeviRepository(TeachABitContext context) : ITecajeviRepository
    {
        private readonly TeachABitContext _context = context;


        /*public async Task<List<Tecaj>> GetTecajList()
        {
            return await _context.Tecajevi.ToListAsync();
        }*/

        public async Task<Tecaj?> GetTecaj(int id)
        {
            //return await _context.Tecajevi.FirstOrDefaultAsync(x => x.Id == id);
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
        public async Task<List<Tecaj>> GetTecajList(string? search = null, string? korisnikId = null)
        {
            var query = _context.Tecajevi
                .Include(x => x.Vlasnik)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                string lowerSearch = search.ToLower();
                query = query.Where(t => t.Naziv.ToLower().Contains(lowerSearch));
            }

            if (!string.IsNullOrEmpty(korisnikId))
            {
                query = query.Include(x => x.TecajPlacanja
                    .Where(t => t.KorisnikId == korisnikId));
            }

            return await query.ToListAsync();
        }
        public async Task<List<Tecaj>> GetTecajListByFiltratingCijena(int maxCijena, int minCijena, string? korisnikId = null)
        {
            var query = _context.Tecajevi
                .Include(x => x.Vlasnik)
                .AsQueryable();

            if (maxCijena>minCijena)
            {
                
                query = query.Where(t => t.Cijena>=minCijena && t.Cijena<=maxCijena);
            }

            if (minCijena == 0)
            {
                query = query.Where(t => t.Cijena <= maxCijena);
            }

            if (maxCijena == minCijena)
            {
                query = query.Where(t => t.Cijena == maxCijena);
            }
            if (!string.IsNullOrEmpty(korisnikId))
            {
                query = query.Include(x => x.TecajPlacanja
                    .Where(t => t.KorisnikId == korisnikId));
            }

            return await query.ToListAsync();
        }
        public async Task<List<Tecaj>> GetTecajListByFiltratingOcjena(int ocjena, string? korisnikId = null)
        {
            var query = _context.Tecajevi
                .Include(x => x.Vlasnik)
                .AsQueryable();
            
                query = query.Where(t => t.Ocjena==ocjena);
                
            if (!string.IsNullOrEmpty(korisnikId))
            {
                query = query.Include(x => x.TecajPlacanja
                    .Where(t => t.KorisnikId == korisnikId));
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

        public async Task<KomentarTecaj> CreateKomentarTecaj(KomentarTecaj komentarTecaj)
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

        public async Task<KomentarTecaj> UpdateKomentarTecaj(KomentarTecaj komentar)
        {
            await _context.SaveChangesAsync();
            return komentar;
        }

        public async Task<KomentarTecaj?> GetKomentarTecajById(int id)
        {
            return await _context.KomentariTecaj.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<KomentarTecaj>> GetKomentarTecajListByTecajId(int id)
        {
            return await _context.KomentariTecaj
                .Include(x => x.Vlasnik)
                .Include(x => x.Tecaj)
                .Include(x => x.KomentarReakcijaList)
                .Where(x => x.Tecaj.Id == id)
                .OrderByDescending(x => x.CreatedDateTime)
                .ToListAsync();
        }

        public async Task<List<KomentarTecaj>> GetPodKomentarTecajList(int tecajId, int? nadKomentarTecajId = null)
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

        public async Task<KomentarTecaj?> GetKomentarTecajByIdWithTracking(int id)
        {
            return await _context.KomentariTecaj.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> HasPodkomentari(int komentarId)
        {
            return await _context.KomentariTecaj.AnyAsync(x => x.NadKomentarId == komentarId);
        }
    }
}