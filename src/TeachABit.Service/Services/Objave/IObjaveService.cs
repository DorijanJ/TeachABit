using TeachABit.Model.DTOs.Objave;
using TeachABit.Model.DTOs.Result;

namespace TeachABit.Service.Services.Objave
{
    public interface IObjaveService
    {
        public Task<ServiceResult<ObjavaDto>> CreateObjava(ObjavaDto objava);
        public Task<ServiceResult<ObjavaDto>> UpdateObjava(ObjavaDto objava);
        public Task<ServiceResult> DeleteObjava(int id);
        public Task<ServiceResult<List<ObjavaDto>>> GetObjavaList(string? search, string? username);
        public Task<ServiceResult<ObjavaDto?>> GetObjavaById(int id);
        public Task<ServiceResult<KomentarDto>> CreateKomentar(KomentarDto komentar, int objavaId);
        public Task<ServiceResult<List<KomentarDto>>> GetKomentarListRecursive(int id, int? nadKomentarId = null);
        public Task<ServiceResult> DeleteKomentar(int id);
        public Task<ServiceResult> LikeObjava(int id);
        public Task<ServiceResult> DislikeObjava(int id);
        public Task<ServiceResult> LikeKomentar(int id);
        public Task<ServiceResult> DislikeKomentar(int id);
        public Task<ServiceResult> ClearObjavaReaction(int id);
        public Task<ServiceResult> ClearKomentarReaction(int id);
    }
}
