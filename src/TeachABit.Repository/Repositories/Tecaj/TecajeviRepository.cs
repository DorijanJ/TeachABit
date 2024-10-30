using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TeachABit.Model;
namespace TeachABit.Repository.Repositories.Tecaj;
using TeachABit.Model.Models.Tecaj;
public class TecajeviRepository(TeachABitContext context) : ITecajeviRepository
{
    private readonly TeachABitContext _context = context;

    public async Task<List<Tecaj>> GetTecajList()
    {
        return await _context.Tecajevi.ToListAsync();
    }
    // Vraća tip Zadatak? jer postoji mogućnost vraćacanja null vrijednosti ako ne postoji podatak sa zadanim id-om.
    /*public async Task<Tecaj?> GetTecaj(int id)
    {
        return await _context.Tecajevi.FirstOrDefaultAsync(x => x.Id == id);
    }*/
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
    
}