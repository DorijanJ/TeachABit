using Microsoft.EntityFrameworkCore;
using TeachABit.Model;
using TeachABit.Model.Models.Objave;

namespace TeachABit.Repository.Repositories.Objave
{
    public class ObjaveRepository(TeachABitContext context) : IObjaveRepository
    {
        private readonly TeachABitContext _context = context;

        public async Task<Komentar> CreateKomentar(Komentar komentar)
        {
            var createdKomentar = await _context.Komentari.AddAsync(komentar);
            await _context.SaveChangesAsync();
            return createdKomentar.Entity;
        }

        public async Task<Objava> CreateObjava(Objava objava)
        {
            var createdObjava = await _context.Objave.AddAsync(objava);
            await _context.SaveChangesAsync();
            return createdObjava.Entity;
        }

        public async Task DeleteKomentar(int id, bool keepEntry = false)
        {
            if (keepEntry)
                await _context.Komentari.Where(x => x.Id == id).ExecuteUpdateAsync(x => x.SetProperty(p => p.IsDeleted, true));
            else
                await _context.Komentari.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task DeleteObjava(int id)
        {
            await _context.Objave.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<Komentar?> GetKomentarById(int id)
        {
            return await _context.Komentari.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Komentar>> GetKomentarListByObjavaId(int id)
        {
            return await _context.Komentari
                .Include(x => x.Vlasnik)
                .Include(x => x.Objava)
                .Include(x => x.KomentarReakcijaList)
                .Where(x => x.Objava.Id == id)
                .OrderByDescending(x => x.CreatedDateTime)
                .ToListAsync();
        }

        public async Task<List<Komentar>> GetPodKomentarList(int objavaId, int? nadKomentarId = null)
        {
            var komentari = await _context.Komentari
                .Include(c => c.Vlasnik)
                .Include(c => c.Objava)
                .Include(c => c.KomentarReakcijaList)
                .Where(c => c.ObjavaId == objavaId && c.NadKomentarId == nadKomentarId)
                .OrderByDescending(c => c.CreatedDateTime)
                .ToListAsync();

            return komentari;
        }

        public async Task<bool> HasPodkomentari(int komentarId)
        {
            return await _context.Komentari.AnyAsync(x => x.NadKomentarId == komentarId);
        }


        public async Task<Objava?> GetObjavaById(int id)
        {
            return await _context.Objave
                .Include(x => x.Vlasnik)
                .Include(x => x.Komentari)
                .ThenInclude(x => x.Vlasnik)
                .Include(x => x.ObjavaReakcijaList)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Objava?> GetObjavaByIdWithTracking(int id)
        {
            return await _context.Objave.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Objava>> GetObjavaList(string? search, string? username)
        {
            IQueryable<Objava> objaveQuery = _context.Objave
             .Include(x => x.Vlasnik)
             .Include(x => x.ObjavaReakcijaList)
             .AsQueryable();


            if (!string.IsNullOrEmpty(search))
            {
                string lowerSearch = search.ToLower();
                objaveQuery = objaveQuery.Where(x => x.Naziv.ToLower().Contains(lowerSearch));
            }

            if (!string.IsNullOrEmpty(username))
            {
                objaveQuery = objaveQuery.Where(x => x.Vlasnik.UserName == username);
            }

            return await objaveQuery.OrderByDescending(x => x.CreatedDateTime).ToListAsync();
        }

        public async Task<Objava> UpdateObjava(Objava objava)
        {
            await _context.SaveChangesAsync();
            return objava;
        }

        public async Task<ObjavaReakcija> CreateObjavaReakcija(ObjavaReakcija objavaReakcija)
        {
            var createdObjavaReakcija = await _context.ObjavaReakcije.AddAsync(objavaReakcija);
            await _context.SaveChangesAsync();
            return createdObjavaReakcija.Entity;
        }

        public async Task DeleteObjavaReakcija(int objavaId, string korisnikId)
        {
            await _context.ObjavaReakcije
                .Where(x => x.KorisnikId == korisnikId && x.ObjavaId == objavaId)
                .ExecuteDeleteAsync();
        }

        public async Task<ObjavaReakcija?> GetObjavaReakcija(int objavaId, string korisnikId)
        {
            return await _context.ObjavaReakcije.FirstOrDefaultAsync(x => x.ObjavaId == objavaId && x.KorisnikId == korisnikId);
        }

        public async Task DeleteObjavaReakcija(int id)
        {
            await _context.ObjavaReakcije.Where(x => x.Id == id).ExecuteDeleteAsync();
        }

        public async Task<int> GetObjavaLikeCount(int id)
        {
            return await _context.ObjavaReakcije.Where(x => x.ObjavaId == id).Select(x => x.Liked ? 1 : -1).SumAsync();
        }

        public async Task DeleteKomentarReakcija(int komentarId, string korisnikId)
        {
            await _context.KomentarReakcije
               .Where(x => x.KorisnikId == korisnikId && x.KomentarId == komentarId)
               .ExecuteDeleteAsync();
        }

        public async Task DeleteKomentarReakcija(int id)
        {
            await _context.KomentarReakcije
               .Where(x => x.Id == id)
               .ExecuteDeleteAsync();
        }

        public async Task<KomentarReakcija> CreateKomentarReakcija(KomentarReakcija komentarReakcija)
        {
            var createdKomentarReakcija = await _context.KomentarReakcije.AddAsync(komentarReakcija);
            await _context.SaveChangesAsync();
            return createdKomentarReakcija.Entity;
        }

        public async Task<KomentarReakcija?> GetKomentarReakcija(int komentarId, string korisnikId)
        {
            return await _context.KomentarReakcije.FirstOrDefaultAsync(x => x.KomentarId == komentarId && x.KorisnikId == korisnikId);
        }

        public async Task<Komentar> UpdateKomentar(Komentar komentar)
        {
            await _context.SaveChangesAsync();
            return komentar;
        }

        public async Task<Komentar?> GetKomentarByIdWithTracking(int id)
        {
            return await _context.Komentari.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
