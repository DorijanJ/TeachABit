using TeachABit.Model.DTOs.Objave;
using TeachABit.Model.DTOs.Result;

namespace TeachABit.Service.Services.Objave
{
    public interface IObjaveService
    {
        #region Objava
        public Task<ServiceResult<ObjavaDto>> CreateObjava(ObjavaDto objava);
        public Task<ServiceResult<ObjavaDto>> UpdateObjava(UpdateObjavaDto updateObjava);
        public Task<ServiceResult> DeleteObjava(int objavaId);
        public Task<ServiceResult<ObjavaDto?>> GetObjavaById(int objavaId);
        public Task<ServiceResult<List<ObjavaDto>>> GetObjavaList(string? search, string? username);
        #endregion

        #region ObjavaReakcija
        public Task<ServiceResult> LikeObjava(int objavaId);
        public Task<ServiceResult> DislikeObjava(int objavaId);
        public Task<ServiceResult> ClearObjavaReakcija(int objavaId);
        #endregion

        #region ObjavaKomentar
        public Task<ServiceResult<ObjavaKomentarDto>> CreateObjavaKomentar(ObjavaKomentarDto komentar, int objavaId);
        public Task<ServiceResult<List<ObjavaKomentarDto>>> GetObjavaKomentarListRecursive(int objavaId, int? nadKomentarId = null);
        public Task<ServiceResult<ObjavaKomentarDto>> UpdateObjavaKomentar(UpdateObjavaKomentarDto updateKomentar);
        public Task<ServiceResult<ObjavaKomentarDto>> OznaciKaoTocan(int komentarId);
        public Task<ServiceResult<ObjavaKomentarDto>> ClearTocanKomentar(int komentarId);
        #endregion

        #region ObjavaKomentarReakcija
        public Task<ServiceResult> DeleteObjavaKomentar(int komentarId);
        public Task<ServiceResult> LikeObjavaKomentar(int komentarId);
        public Task<ServiceResult> DislikeObjavaKomentar(int komentarId);
        public Task<ServiceResult> ClearObjavaKomentarReaction(int komentarId);
        #endregion
    }
}
