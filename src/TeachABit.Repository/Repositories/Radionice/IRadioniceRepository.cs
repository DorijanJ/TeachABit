using TeachABit.Model.Models.Radionice;

namespace TeachABit.Repository.Repositories.Radionice;

public interface IRadioniceRepository
{
    Task<List<Radionica>> GetRadionicaList();
    Task<Radionica?> GetRadionica(int id);
    Task<Radionica> CreateRadionica(Radionica radionica);
    //Task<Radionica> UpdateRadionica(Radionica zadatak);
    Task DeleteRadionica(int id);
}