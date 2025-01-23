using TeachABit.Model.Models.Objave;

namespace TeachABit.Repository.Repositories.Objave
{
    public interface IObjaveRepository
    {
        #region Objava
        public Task<Objava> CreateObjava(Objava objava);
        public Task<Objava> UpdateObjava(Objava objava);
        public Task DeleteObjava(int objavaId);
        public Task<Objava?> GetObjavaById(int objavaId);
        public Task<Objava?> GetObjavaByIdWithTracking(int objavaId);
        public Task<List<Objava>> GetObjavaList(string? searchTerm, string? username);
        public Task<int> GetObjavaLikeCount(int objavaId);
        #endregion
        #region ObjavaReakcija
        public Task<ObjavaReakcija> CreateObjavaReakcija(ObjavaReakcija reakcija);
        public Task<ObjavaReakcija?> GetObjavaReakcija(int objavaId, string korisnikId);
        public Task DeleteObjavaReakcija(int objavaId, string korisnikId);
        public Task DeleteObjavaReakcijaById(int reakcijaId);
        #endregion
        #region Komentar
        public Task<ObjavaKomentar> CreateKomentar(ObjavaKomentar komentar);
        public Task<ObjavaKomentar> UpdateKomentar(ObjavaKomentar komentar);
        public Task DeleteKomentar(int komentarId, bool keepEntry = false);
        public Task<ObjavaKomentar?> GetKomentarById(int komentarId);
        public Task<ObjavaKomentar?> GetKomentarByIdWithTracking(int komentarId);
        public Task<List<ObjavaKomentar>> GetKomentarListByObjavaId(int objavaId);
        public Task<List<ObjavaKomentar>> GetPodKomentarList(int objavaId, int? nadKomentarId = null);
        public Task<bool> HasPodkomentari(int komentarId);
        public Task<ObjavaKomentar?> GetTocanKomentar(int objavaId);
        #endregion
        #region KomentarReakcija
        public Task<ObjavaKomentarReakcija> CreateKomentarReakcija(ObjavaKomentarReakcija reakcija);
        public Task<ObjavaKomentarReakcija?> GetKomentarReakcija(int komentarId, string korisnikId);
        public Task DeleteKomentarReakcija(int komentarId, string korisnikId);
        public Task DeleteKomentarReakcijaById(int reakcijaId);
        #endregion
    }
}
