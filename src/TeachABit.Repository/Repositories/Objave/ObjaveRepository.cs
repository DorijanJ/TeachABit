using Microsoft.EntityFrameworkCore;
using TeachABit.Model;
using TeachABit.Model.Models.Objave;

namespace TeachABit.Repository.Repositories.Objave
{
    public class ObjaveRepository(TeachABitContext context) : IObjaveRepository
    {
        private readonly TeachABitContext _context = context;

        #region Objava
        public async Task<Objava> CreateObjava(Objava objava)
        {
            var createdObjava = await _context.Objave.AddAsync(objava);
            await _context.SaveChangesAsync();
            return createdObjava.Entity;
        }

        public async Task<Objava> UpdateObjava(Objava objava)
        {
            await _context.SaveChangesAsync();
            return objava;
        }

        public async Task DeleteObjava(int objavaId)
        {
            await _context.Objave.Where(x => x.Id == objavaId).ExecuteDeleteAsync();
        }

        public async Task<Objava?> GetObjavaById(int objavaId)
        {
            return await _context.Objave
                .Include(x => x.Vlasnik)
                .Include(x => x.Komentari)
                .ThenInclude(x => x.Vlasnik)
                .Include(x => x.ObjavaReakcijaList)
                .FirstOrDefaultAsync(x => x.Id == objavaId);
        }

        public async Task<Objava?> GetObjavaByIdWithTracking(int objavaId)
        {
            return await _context.Objave.AsTracking().FirstOrDefaultAsync(x => x.Id == objavaId);
        }

        public async Task<List<Objava>> GetObjavaList(string? searchTerm, string? korisnickoIme)
        {
            IQueryable<Objava> objaveQuery = _context.Objave
                .Include(x => x.Vlasnik)
                .Include(x => x.ObjavaReakcijaList);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                string lowerSearch = searchTerm.ToLower();
                objaveQuery = objaveQuery.Where(x => x.Naziv.ToLower().Contains(lowerSearch));
            }

            if (!string.IsNullOrEmpty(korisnickoIme))
            {
                objaveQuery = objaveQuery.Where(x => x.Vlasnik.UserName == korisnickoIme);
            }

            return await objaveQuery.OrderByDescending(x => x.CreatedDateTime).ToListAsync();
        }

        public async Task<int> GetObjavaLikeCount(int objavaId)
        {
            return await _context.ObjavaReakcije
                .Where(x => x.ObjavaId == objavaId)
                .Select(x => x.Liked ? 1 : -1)
                .SumAsync();
        }
        #endregion
        #region ObjavaReakcija
        public async Task<ObjavaReakcija> CreateObjavaReakcija(ObjavaReakcija objavaReakcija)
        {
            var createdObjavaReakcija = await _context.ObjavaReakcije.AddAsync(objavaReakcija);
            await _context.SaveChangesAsync();
            return createdObjavaReakcija.Entity;
        }

        public async Task<ObjavaReakcija?> GetObjavaReakcija(int objavaId, string korisnikId)
        {
            return await _context.ObjavaReakcije
                .FirstOrDefaultAsync(x => x.ObjavaId == objavaId && x.KorisnikId == korisnikId);
        }

        public async Task DeleteObjavaReakcija(int objavaId, string korisnikId)
        {
            await _context.ObjavaReakcije
                .Where(x => x.KorisnikId == korisnikId && x.ObjavaId == objavaId)
                .ExecuteDeleteAsync();
        }

        public async Task DeleteObjavaReakcijaById(int reakcijaId)
        {
            await _context.ObjavaReakcije
                .Where(x => x.Id == reakcijaId)
                .ExecuteDeleteAsync();
        }
        #endregion
        #region ObjavaKomentar
        public async Task<ObjavaKomentar> CreateKomentar(ObjavaKomentar komentar)
        {
            var createdKomentar = await _context.Komentari.AddAsync(komentar);
            await _context.SaveChangesAsync();
            return createdKomentar.Entity;
        }

        public async Task<ObjavaKomentar> UpdateKomentar(ObjavaKomentar komentar)
        {
            await _context.SaveChangesAsync();
            return komentar;
        }

        public async Task DeleteKomentar(int komentarId, bool keepEntry = false)
        {
            if (keepEntry)
            {
                await _context.Komentari
                    .Where(x => x.Id == komentarId)
                    .ExecuteUpdateAsync(x => x.SetProperty(p => p.IsDeleted, true));
            }
            else
            {
                await _context.Komentari.Where(x => x.Id == komentarId).ExecuteDeleteAsync();
            }
        }

        public async Task<ObjavaKomentar?> GetKomentarById(int komentarId)
        {
            return await _context.Komentari.FirstOrDefaultAsync(x => x.Id == komentarId);
        }

        public async Task<ObjavaKomentar?> GetKomentarByIdWithTracking(int komentarId)
        {
            return await _context.Komentari.AsTracking().FirstOrDefaultAsync(x => x.Id == komentarId);
        }

        public async Task<List<ObjavaKomentar>> GetKomentarListByObjavaId(int objavaId)
        {
            return await _context.Komentari
                .Include(x => x.Vlasnik)
                .Include(x => x.Objava)
                .Include(x => x.KomentarReakcijaList)
                .Where(x => x.Objava.Id == objavaId)
                .OrderByDescending(x => x.OznacenTocan)
                .ThenByDescending(x => x.CreatedDateTime)
                .ToListAsync();
        }

        public async Task<List<ObjavaKomentar>> GetPodKomentarList(int objavaId, int? nadKomentarId = null)
        {
            var komentari = _context.Komentari
                .Include(c => c.Vlasnik)
                .Include(c => c.Objava)
                .Include(c => c.KomentarReakcijaList)
                .Where(c => c.ObjavaId == objavaId && c.NadKomentarId == nadKomentarId)
                .AsQueryable();

            komentari = nadKomentarId == null
                ? komentari.OrderBy(c => c.OznacenTocan).ThenByDescending(c => c.CreatedDateTime)
                : komentari.OrderByDescending(c => c.CreatedDateTime);

            return await komentari.ToListAsync();
        }

        public async Task<bool> HasPodkomentari(int komentarId)
        {
            return await _context.Komentari.AnyAsync(x => x.NadKomentarId == komentarId);
        }

        public async Task<ObjavaKomentar?> GetTocanKomentar(int objavaId)
        {
            return await _context.Komentari
                .AsTracking()
                .FirstOrDefaultAsync(x => x.ObjavaId == objavaId && x.OznacenTocan == true);
        }
        #endregion
        #region ObjavaKomentarReakcija
        public async Task<ObjavaKomentarReakcija> CreateKomentarReakcija(ObjavaKomentarReakcija reakcija)
        {
            var createdKomentarReakcija = await _context.KomentarReakcije.AddAsync(reakcija);
            await _context.SaveChangesAsync();
            return createdKomentarReakcija.Entity;
        }

        public async Task<ObjavaKomentarReakcija?> GetKomentarReakcija(int komentarId, string korisnikId)
        {
            return await _context.KomentarReakcije
                .FirstOrDefaultAsync(x => x.KomentarId == komentarId && x.KorisnikId == korisnikId);
        }

        public async Task DeleteKomentarReakcija(int komentarId, string korisnikId)
        {
            await _context.KomentarReakcije
                .Where(x => x.KorisnikId == korisnikId && x.KomentarId == komentarId)
                .ExecuteDeleteAsync();
        }

        public async Task DeleteKomentarReakcijaById(int reakcijaId)
        {
            await _context.KomentarReakcije
                .Where(x => x.Id == reakcijaId)
                .ExecuteDeleteAsync();
        }
        #endregion
    }
}
