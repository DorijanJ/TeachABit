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

        public async Task<ServiceResult> ClearKomentarReaction(int id)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();
            await _objaveRepository.DeleteKomentarReakcija(id, korisnik.Id);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> ClearObjavaReaction(int id)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();
            await _objaveRepository.DeleteObjavaReakcija(id, korisnik.Id);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<KomentarDto>> CreateKomentar(KomentarDto komentar, int objavaId)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            komentar.VlasnikId = korisnik.Id;
            komentar.CreatedDateTime = DateTime.UtcNow;
            komentar.ObjavaId = objavaId;

            KomentarDto createdKomentar = _mapper.Map<KomentarDto>(await _objaveRepository.CreateKomentar(_mapper.Map<Komentar>(komentar)));
            return ServiceResult<KomentarDto>.Success(createdKomentar);
        }

        public async Task<ServiceResult<ObjavaDto>> CreateObjava(ObjavaDto objava)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            objava.VlasnikId = korisnik.Id;

            ObjavaDto createdObjava = _mapper.Map<ObjavaDto>(await _objaveRepository.CreateObjava(_mapper.Map<Objava>(objava)));
            return ServiceResult<ObjavaDto>.Success(createdObjava);
        }

        public async Task<ServiceResult> DeleteKomentar(int id)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            KomentarDto? komentar = _mapper.Map<KomentarDto>(await _objaveRepository.GetKomentarById(id));

            if (komentar == null || !korisnik.Owns(komentar)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            await _objaveRepository.DeleteKomentar(id);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DeleteKomentar(int id)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            KomentarDto? komentar = _mapper.Map<KomentarDto>(await _objaveRepository.GetKomentarById(id));

            if (komentar == null || !korisnik.Owns(komentar)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            await _objaveRepository.DeleteKomentar(id);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DeleteObjava(int id)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            ObjavaDto? objava = _mapper.Map<ObjavaDto?>(await _objaveRepository.GetObjavaById(id));

            if (objava == null || !korisnik.Owns(objava)) return ServiceResult.Failure(MessageDescriber.Unauthorized());
            if (objava == null || !korisnik.Owns(objava)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            await _objaveRepository.DeleteObjava(id);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DislikeKomentar(int id)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            var exisitingKomentarReakcija = await _objaveRepository.GetKomentarReakcija(id, korisnik.Id);

            if (exisitingKomentarReakcija != null)
            {
                if (exisitingKomentarReakcija.Liked == false)
                    return ServiceResult.Success();

                await _objaveRepository.DeleteKomentarReakcija(exisitingKomentarReakcija.Id);
            }

            KomentarReakcija komentarReakcija = new()
            {
                KorisnikId = korisnik.Id,
                KomentarId = id,
                Liked = false,
            };

            await _objaveRepository.CreateKomentarReakcija(komentarReakcija);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DislikeObjava(int id)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            var existingObjavaReakcija = await _objaveRepository.GetObjavaReakcija(id, korisnik.Id);

            if (existingObjavaReakcija != null)
            {
                if (existingObjavaReakcija.Liked == false)
                    return ServiceResult.Success();

                await _objaveRepository.DeleteObjavaReakcija(existingObjavaReakcija.Id);
            }

            ObjavaReakcija objavaReakcija = new()
            {
                KorisnikId = korisnik.Id,
                ObjavaId = id,
                Liked = false,
            };

            await _objaveRepository.CreateObjavaReakcija(objavaReakcija);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<KomentarDto>>> GetKomentarListRecursive(int id, int? nadKomentarId = null)
        {
            List<KomentarDto> komentari = _mapper.Map<List<KomentarDto>>(await _objaveRepository.GetPodKomentarList(id, nadKomentarId));
            Korisnik? korisnik = _authorizationService.GetKorisnikOptional();

            foreach (var komentar in komentari)
            {
                if (korisnik != null)
                {
                    komentar.Liked = (await _objaveRepository.GetKomentarReakcija(komentar.Id, korisnik.Id))?.Liked;
                }
                komentar.PodKomentarList = _mapper.Map<List<KomentarDto>>((await GetKomentarListRecursive(id, komentar.Id)).Data);
            }

            return ServiceResult<List<KomentarDto>>.Success(komentari);
        }

        public async Task<ServiceResult<ObjavaDto?>> GetObjavaById(int id)
        {
            ObjavaDto? objava = _mapper.Map<ObjavaDto?>(await _objaveRepository.GetObjavaById(id));

            if (objava == null) return ServiceResult<ObjavaDto?>.Failure(MessageDescriber.ItemNotFound());

            Korisnik? korisnik = _authorizationService.GetKorisnikOptional();
            if (korisnik != null)
                objava.Liked = (await _objaveRepository.GetObjavaReakcija(id, korisnik.Id))?.Liked;


            return ServiceResult<ObjavaDto?>.Success(objava);
        }

        public async Task<ServiceResult<List<ObjavaDto>>> GetObjavaList(string? search, string? username)
        public async Task<ServiceResult<List<ObjavaDto>>> GetObjavaList(string? search, string? username)
        {
            List<Objava> objave = await _objaveRepository.GetObjavaList(search, username);
            List<ObjavaDto> objavaDtoList = [];

            Korisnik? korisnik = _authorizationService.GetKorisnikOptional();

            if (korisnik == null) return ServiceResult<List<ObjavaDto>>.Success(_mapper.Map<List<ObjavaDto>>(objave));

            foreach (var objava in objave)
            {
                ObjavaDto objavaDto = _mapper.Map<ObjavaDto>(objava);
                var objavaReaction = await _objaveRepository.GetObjavaReakcija(objava.Id, korisnik.Id);
                objavaDto.Liked = objavaReaction?.Liked;
                objavaDtoList.Add(objavaDto);
            }

            return ServiceResult<List<ObjavaDto>>.Success(objavaDtoList);
        }

        public async Task<ServiceResult> LikeKomentar(int id)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            var exisitingKomentarReakcija = await _objaveRepository.GetKomentarReakcija(id, korisnik.Id);

            if (exisitingKomentarReakcija != null)
            {
                if (exisitingKomentarReakcija.Liked == true)
                    return ServiceResult.Success();

                await _objaveRepository.DeleteKomentarReakcija(exisitingKomentarReakcija.Id);
            }

            KomentarReakcija komentarReakcija = new()
            {
                KorisnikId = korisnik.Id,
                KomentarId = id,
                Liked = true,
            };

            await _objaveRepository.CreateKomentarReakcija(komentarReakcija);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> LikeObjava(int id)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            var existingObjavaReakcija = await _objaveRepository.GetObjavaReakcija(id, korisnik.Id);

            if (existingObjavaReakcija != null)
            {
                if (existingObjavaReakcija.Liked == true)
                    return ServiceResult.Success();

                await _objaveRepository.DeleteObjavaReakcija(existingObjavaReakcija.Id);
            }

            ObjavaReakcija objavaReakcija = new()
            {
                KorisnikId = korisnik.Id,
                ObjavaId = id,
                Liked = true,
            };

            await _objaveRepository.CreateObjavaReakcija(objavaReakcija);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<ObjavaDto>> UpdateObjava(ObjavaDto objava)
        {
            ObjavaDto updatedObjava = _mapper.Map<ObjavaDto>(await _objaveRepository.UpdateObjava(_mapper.Map<Objava>(objava)));
            return ServiceResult<ObjavaDto>.Success(updatedObjava);
        }
    }
}
