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
        /*public async Task<Tecaj> UpdateTecaj(Tecaj tecaj)
        {
            // Moram provjeriti najbolji način implementacije za update.
            ...
        }*/
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
        public async Task<List<Tecaj>> GetTecajList(string? search = null)
        {
            if (!string.IsNullOrEmpty(search))
            {
                string lowerSearch = search.ToLower();
                return await _context.Tecajevi
                    .Where(t => t.Naziv.ToLower().Contains(lowerSearch))
                    .ToListAsync();
            }
            return await _context.Tecajevi.ToListAsync();
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
            if (keepEntry)
                await _context.Lekcije.Where(x => x.Id == id).ExecuteUpdateAsync(x => x.SetProperty(p => p.IsDeleted, true));
            else
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

    }
}