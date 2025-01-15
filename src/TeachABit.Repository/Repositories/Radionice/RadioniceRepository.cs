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
        return await _context.Radionice
            .Include(x => x.Vlasnik)
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
    
}