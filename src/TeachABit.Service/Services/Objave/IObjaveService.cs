using TeachABit.Model.DTOs.Objave;
using TeachABit.Model.DTOs.Result;

namespace TeachABit.Service.Services.Objave
{
    public interface IObjaveService
    {
        public Task<ServiceResult<ObjavaDto>> CreateObjava(ObjavaDto objava);
        public Task<ServiceResult<ObjavaDto>> UpdateObjava(ObjavaDto objava);
        public Task<ServiceResult> DeleteObjava(int id);
        public Task<ServiceResult<List<ObjavaDto>>> GetObjavaList(string? search);
        public Task<ServiceResult<DetailedObjavaDto?>> GetObjavaById(int id);
    }
}
