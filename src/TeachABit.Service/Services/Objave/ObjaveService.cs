using AutoMapper;
using TeachABit.Model.DTOs.Objave;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.Mapping;
using TeachABit.Repository.Repositories.Objave;

namespace TeachABit.Service.Services.Objave
{
    public class ObjaveService(IObjaveRepository objaveRepository, IMapper mapper) : IObjaveService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IObjaveRepository _objaveRepository = objaveRepository;
        public Task<ServiceResult<ObjavaDto>> CreateObjava(ObjavaDto objava)
        { 
            return _mapper.Map<ObjavaDto>
        }

        public Task<ServiceResult> DeleteObjava(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<ObjavaDto?>> GetObjavaById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<List<ObjavaDto>>> GetObjavaList()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<ObjavaDto>> UpdateObjava(ObjavaDto objava)
        {
            throw new NotImplementedException();
        }
    }
}
