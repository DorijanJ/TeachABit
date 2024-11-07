using AutoMapper;
using TeachABit.Model.DTOs.Objave;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Model.Models.Korisnici.Extensions;
using TeachABit.Model.Models.Objave;
using TeachABit.Repository.Repositories.Objave;
using TeachABit.Service.Services.Authorization;

namespace TeachABit.Service.Services.Objave
{
    public class ObjaveService(IObjaveRepository objaveRepository, IMapper mapper, IAuthorizationService authorizationService) : IObjaveService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IObjaveRepository _objaveRepository = objaveRepository;
        private readonly IAuthorizationService _authorizationService = authorizationService;
        public async Task<ServiceResult<ObjavaDto>> CreateObjava(ObjavaDto objava)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            objava.VlasnikId = korisnik.Id;

            ObjavaDto createdObjava = _mapper.Map<ObjavaDto>(await _objaveRepository.CreateObjava(_mapper.Map<Objava>(objava)));
            return ServiceResult<ObjavaDto>.Success(createdObjava);
        }

        public async Task<ServiceResult> DeleteObjava(int id)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            ObjavaDto? objava = _mapper.Map<ObjavaDto?>(await _objaveRepository.GetObjavaById(id));

            if (objava == null || !korisnik.Owns(objava)) return ServiceResult.Failure(MessageDescriber.ItemNotFound());

            await _objaveRepository.DeleteObjava(id);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<DetailedObjavaDto?>> GetObjavaById(int id)
        {
            DetailedObjavaDto? objava = _mapper.Map<DetailedObjavaDto?>(await _objaveRepository.GetObjavaById(id));
            return ServiceResult<DetailedObjavaDto?>.Success(objava);
        }

        public async Task<ServiceResult<List<ObjavaDto>>> GetObjavaList(string? search)
        {
            List<ObjavaDto> objave = _mapper.Map<List<ObjavaDto>>(await _objaveRepository.GetObjavaList(search));
            return ServiceResult<List<ObjavaDto>>.Success(objave);
        }

        public async Task<ServiceResult<ObjavaDto>> UpdateObjava(ObjavaDto objava)
        {
            ObjavaDto updatedObjava = _mapper.Map<ObjavaDto>(await _objaveRepository.UpdateObjava(_mapper.Map<Objava>(objava)));
            return ServiceResult<ObjavaDto>.Success(updatedObjava);
        }
    }
}
