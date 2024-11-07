using TeachABit.Model.Models.Objave;

namespace TeachABit.Repository.Repositories.Objave
{
    public interface IObjaveRepository
    {
        public Task<Objava> CreateObjava(Objava objava);
        public Task<Objava> UpdateObjava(Objava objava);
        public Task DeleteObjava(int id);
        public Task<List<Objava>> GetObjavaList(string? search);
        public Task<Objava?> GetObjavaById(int id);
    }
}
