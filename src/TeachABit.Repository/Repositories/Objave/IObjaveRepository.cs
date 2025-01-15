using TeachABit.Model.Models.Objave;

namespace TeachABit.Repository.Repositories.Objave
{
    public interface IObjaveRepository
    {
        public Task<Objava> CreateObjava(Objava objava);
        public Task<Objava> UpdateObjava(Objava objava);
        public Task DeleteObjava(int id);
        public Task<List<Objava>> GetObjavaList(string? search, string? username);
        public Task<Objava?> GetObjavaById(int id);
        public Task<Komentar> CreateKomentar(Komentar komentar);
        public Task DeleteKomentar(int id, bool keepEntry = false);
        public Task<Komentar> UpdateKomentar(Komentar komentar);
        public Task<Komentar?> GetKomentarById(int id);
        public Task<List<Komentar>> GetKomentarListByObjavaId(int id);
        public Task<List<Komentar>> GetPodKomentarList(int objavaId, int? nadKomentarId = null);
        public Task<ObjavaReakcija> CreateObjavaReakcija(ObjavaReakcija objavaReakcija);
        public Task DeleteObjavaReakcija(int objavaId, string korisnikId);
        public Task DeleteObjavaReakcija(int id);
        public Task<int> GetObjavaLikeCount(int id);
        public Task<ObjavaReakcija?> GetObjavaReakcija(int objavaId, string korisnikId);
        public Task DeleteKomentarReakcija(int komentarId, string korisnikId);
        public Task DeleteKomentarReakcija(int id);
        public Task<KomentarReakcija> CreateKomentarReakcija(KomentarReakcija komentarReakcija);
        public Task<KomentarReakcija?> GetKomentarReakcija(int komentarId, string korisnikId);
        public Task<Objava?> GetObjavaByIdWithTracking(int id);
        public Task<Komentar?> GetKomentarByIdWithTracking(int id);
        public Task<bool> HasPodkomentari(int komentarId);
    }
}
