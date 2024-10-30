namespace TeachABit.Repository.Repositories.Tecaj;
using TeachABit.Model.Models.Tecaj;
public interface ITecajeviRepository
{
    Task<List<Tecaj>> GetTecajList();
    Task<Tecaj> GetTecaj(int id);
    Task<Tecaj> CreateTecaj(Tecaj tecaj);
    //Task<Tecaj> UpdateTecaj(Tecaj tecaj);
    Task DeleteTecaj(int id);
}