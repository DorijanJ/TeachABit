using TeachABit.Model.Models.Tecajevi;

namespace TeachABit.Repository.Repositories.Tecajevi
{
    public interface ITecajeviRepository
    {
        Task<List<Tecaj>> GetTecajList();
        Task<Tecaj?> GetTecaj(int id);
        Task<Tecaj> CreateTecaj(Tecaj tecaj);
        //Task<Tecaj> UpdateTecaj(Tecaj tecaj);
        Task DeleteTecaj(int id);
    }
}