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
            return await _context.Tecajevi.FirstOrDefaultAsync(x => x.Id == id);
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


    }
}