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

        public async Task DeleteKomentar(int id)
        {
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
                .Where(x => x.Objava.Id == id)
                .OrderByDescending(x => x.CreatedDateTime)
                .ToListAsync();
        }

        public async Task<List<Komentar>> GetPodKomentarList(int objavaId, int? nadKomentarId = null)
        {
            var komentari = await _context.Komentari
                .Include(c => c.Vlasnik)
                .Include(c => c.Objava)
                .Where(c => c.ObjavaId == objavaId && c.NadKomentarId == nadKomentarId)
                .OrderByDescending(c => c.CreatedDateTime)
                .ToListAsync();

            foreach (var komentar in komentari)
            {
                komentar.PodKomentarList = await GetPodKomentarList(objavaId, komentar.Id);
            }

            return komentari;
        }


        public async Task<Objava?> GetObjavaById(int id)
        {
            return await _context.Objave
                .Include(x => x.Vlasnik)
                .Include(x => x.Komentari)
                .ThenInclude(x => x.Vlasnik)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Objava>> GetObjavaList(string? search, string? username)
        {
            IQueryable<Objava> objaveQuery = _context.Objave
             .Include(x => x.Vlasnik)
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

            return await objaveQuery.ToListAsync();
        }

        public Task<Objava> UpdateObjava(Objava objava)
        {
            // on wait
            throw new NotImplementedException();
        }
    }
}
