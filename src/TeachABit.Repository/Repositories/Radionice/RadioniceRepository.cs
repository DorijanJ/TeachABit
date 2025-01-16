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
}