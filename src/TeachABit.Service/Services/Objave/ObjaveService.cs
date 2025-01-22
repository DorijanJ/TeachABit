using AutoMapper;
using TeachABit.Model.DTOs.Objave;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Model.Enums;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Model.Models.Objave;
using TeachABit.Repository.Repositories.Objave;
using TeachABit.Service.Services.Authorization;

namespace TeachABit.Service.Services.Objave
{
    public class ObjaveService(IObjaveRepository objaveRepository, IMapper mapper, IAuthorizationService authorizationService, IOwnershipService ownershipService) : IObjaveService
    {
        private readonly IMapper _mapper = mapper;
        private readonly IObjaveRepository _objaveRepository = objaveRepository;
        private readonly IAuthorizationService _authorizationService = authorizationService;
        private readonly IOwnershipService _ownershipService = ownershipService;

        #region Objava

        public async Task<ServiceResult<ObjavaDto>> CreateObjava(ObjavaDto objava)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            objava.VlasnikId = korisnik.Id;

            ObjavaDto createdObjava = _mapper.Map<ObjavaDto>(await _objaveRepository.CreateObjava(_mapper.Map<Objava>(objava)));
            return ServiceResult.Success(createdObjava);
        }

        public async Task<ServiceResult<ObjavaDto>> UpdateObjava(UpdateObjavaDto updateObjava)
        {
            var objava = await _objaveRepository.GetObjavaByIdWithTracking(updateObjava.Id);

            if (objava == null || !_ownershipService.Owns(objava)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            objava.Naziv = updateObjava.Naziv;
            objava.Sadrzaj = updateObjava.Sadrzaj;

            var updatedObjava = _mapper.Map<ObjavaDto>(await _objaveRepository.UpdateObjava(objava));

            return ServiceResult.Success(updatedObjava);
        }

        public async Task<ServiceResult> DeleteObjava(int id)
        {
            ObjavaDto? objava = _mapper.Map<ObjavaDto?>(await _objaveRepository.GetObjavaById(id));

            bool isModerator = await _authorizationService.HasPermission(LevelPristupaEnum.Moderator);

            if (objava == null || !(_ownershipService.Owns(objava) || isModerator)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            await _objaveRepository.DeleteObjava(id);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<ObjavaDto?>> GetObjavaById(int id)
        {
            ObjavaDto? objava = _mapper.Map<ObjavaDto?>(await _objaveRepository.GetObjavaById(id));

            if (objava == null) return ServiceResult.Failure(MessageDescriber.ItemNotFound());

            Korisnik? korisnik = _authorizationService.GetKorisnikOptional();
            if (korisnik != null)
                objava.Liked = (await _objaveRepository.GetObjavaReakcija(id, korisnik.Id))?.Liked;

            return ServiceResult.NullableSuccess(objava);
        }

        public async Task<ServiceResult<List<ObjavaDto>>> GetObjavaList(string? search, string? username)
        {
            List<Objava> objave = await _objaveRepository.GetObjavaList(search, username);
            List<ObjavaDto> objavaDtoList = [];

            Korisnik? korisnik = _authorizationService.GetKorisnikOptional();

            if (korisnik == null) return ServiceResult.Success(_mapper.Map<List<ObjavaDto>>(objave));

            foreach (var objava in objave)
            {
                ObjavaDto objavaDto = _mapper.Map<ObjavaDto>(objava);
                var objavaReaction = await _objaveRepository.GetObjavaReakcija(objava.Id, korisnik.Id);
                objavaDto.Liked = objavaReaction?.Liked;
                objavaDtoList.Add(objavaDto);
            }

            return ServiceResult.Success(objavaDtoList);
        }

        #endregion

        #region ObjavaReakcija

        public async Task<ServiceResult> LikeObjava(int objavaId)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            var existingObjavaReakcija = await _objaveRepository.GetObjavaReakcija(objavaId, korisnik.Id);

            if (existingObjavaReakcija != null)
            {
                if (existingObjavaReakcija.Liked == true)
                    return ServiceResult.Success();

                await _objaveRepository.DeleteObjavaReakcijaById(existingObjavaReakcija.Id);
            }

            ObjavaReakcija objavaReakcija = new()
            {
                KorisnikId = korisnik.Id,
                ObjavaId = objavaId,
                Liked = true,
            };

            await _objaveRepository.CreateObjavaReakcija(objavaReakcija);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DislikeObjava(int objavaId)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            var existingObjavaReakcija = await _objaveRepository.GetObjavaReakcija(objavaId, korisnik.Id);

            if (existingObjavaReakcija != null)
            {
                if (existingObjavaReakcija.Liked == false)
                    return ServiceResult.Success();

                await _objaveRepository.DeleteKomentarReakcijaById(existingObjavaReakcija.Id);
            }

            ObjavaReakcija objavaReakcija = new()
            {
                KorisnikId = korisnik.Id,
                ObjavaId = objavaId,
                Liked = false,
            };

            await _objaveRepository.CreateObjavaReakcija(objavaReakcija);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> ClearObjavaReakcija(int objavaId)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            var objava = await _objaveRepository.GetKomentarById(objavaId);

            if (objava == null || !_ownershipService.Owns(objava)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            await _objaveRepository.DeleteObjavaReakcija(objavaId, korisnik.Id);
            return ServiceResult.Success();
        }

        #endregion

        #region ObjavaKomentar

        public async Task<ServiceResult<ObjavaKomentarDto>> CreateObjavaKomentar(ObjavaKomentarDto komentar, int objavaId)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            komentar.VlasnikId = korisnik.Id;
            komentar.CreatedDateTime = DateTime.UtcNow;
            komentar.ObjavaId = objavaId;

            ObjavaKomentarDto createdKomentar = _mapper.Map<ObjavaKomentarDto>(await _objaveRepository.CreateKomentar(_mapper.Map<ObjavaKomentar>(komentar)));
            return ServiceResult.Success(createdKomentar);
        }

        public async Task<ServiceResult<List<ObjavaKomentarDto>>> GetObjavaKomentarListRecursive(int objavaId, int? nadKomentarId = null)
        {
            List<ObjavaKomentarDto> komentari = _mapper.Map<List<ObjavaKomentarDto>>(await _objaveRepository.GetPodKomentarList(objavaId, nadKomentarId));
            Korisnik? korisnik = _authorizationService.GetKorisnikOptional();

            foreach (var komentar in komentari)
            {
                if (korisnik != null)
                {
                    komentar.Liked = (await _objaveRepository.GetKomentarReakcija(komentar.Id, korisnik.Id))?.Liked;
                }
                komentar.PodKomentarList = _mapper.Map<List<ObjavaKomentarDto>>((await GetObjavaKomentarListRecursive(objavaId, komentar.Id)).Data);
            }

            return ServiceResult.Success(komentari);
        }

        public async Task<ServiceResult<ObjavaKomentarDto>> UpdateObjavaKomentar(UpdateObjavaKomentarDto updateKomentar)
        {
            var komentar = await _objaveRepository.GetKomentarByIdWithTracking(updateKomentar.Id);

            if (komentar == null || !_ownershipService.Owns(komentar) || komentar.IsDeleted) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            komentar.Sadrzaj = updateKomentar.Sadrzaj;
            komentar.LastUpdatedDateTime = DateTime.UtcNow;

            var updatedKomentar = _mapper.Map<ObjavaKomentarDto>(await _objaveRepository.UpdateKomentar(komentar));

            return ServiceResult.Success(updatedKomentar);
        }

        public async Task<ServiceResult<ObjavaKomentarDto>> OznaciKaoTocan(int komentarId)
        {
            var komentar = await _objaveRepository.GetKomentarByIdWithTracking(komentarId);

            if (komentar == null) { return ServiceResult.Failure(MessageDescriber.Unauthorized()); }

            var objava = await _objaveRepository.GetObjavaById(komentar.ObjavaId);

            if (objava == null || !_ownershipService.Owns(objava)) { return ServiceResult.Failure(MessageDescriber.Unauthorized()); }

            var vecTocan = await _objaveRepository.GetTocanKomentar(objava.Id);

            if (vecTocan != null)
            {
                vecTocan.OznacenTocan = null;
                await _objaveRepository.UpdateKomentar(vecTocan);
            }

            komentar.OznacenTocan = true;
            await _objaveRepository.UpdateKomentar(komentar);
            return ServiceResult.Success(_mapper.Map<ObjavaKomentarDto>(komentar));
        }

        public async Task<ServiceResult<ObjavaKomentarDto>> ClearTocanKomentar(int komentarId)
        {
            var komentar = await _objaveRepository.GetKomentarByIdWithTracking(komentarId);

            if (komentar == null) { return ServiceResult.Failure(MessageDescriber.Unauthorized()); }

            var objava = await _objaveRepository.GetObjavaById(komentar.ObjavaId);

            if (objava == null || !_ownershipService.Owns(objava)) { return ServiceResult.Failure(MessageDescriber.Unauthorized()); }

            if (komentar.OznacenTocan != true) return ServiceResult.Failure(MessageDescriber.BadRequest("Komentar nije označen kao točan."));

            komentar.OznacenTocan = false;
            await _objaveRepository.UpdateKomentar(komentar);
            return ServiceResult.Success(_mapper.Map<ObjavaKomentarDto>(komentar));
        }

        #endregion

        #region ObjavaKomentarReakcija

        public async Task<ServiceResult> DeleteObjavaKomentar(int komentarId)
        {
            ObjavaKomentar? komentar = await _objaveRepository.GetKomentarById(komentarId);

            var isModerator = await _authorizationService.HasPermission(LevelPristupaEnum.Moderator);

            if (komentar == null || !(_ownershipService.Owns(komentar) || isModerator)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            if (komentar.IsDeleted) return ServiceResult.Failure(MessageDescriber.BadRequest("Komentar je već izbrisan."));

            var hasPodkomentari = await _objaveRepository.HasPodkomentari(komentarId);

            await _objaveRepository.DeleteKomentar(komentarId, keepEntry: hasPodkomentari);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> LikeObjavaKomentar(int komentarId)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            var exisitingKomentarReakcija = await _objaveRepository.GetKomentarReakcija(komentarId, korisnik.Id);

            if (exisitingKomentarReakcija != null)
            {
                if (exisitingKomentarReakcija.Liked == true)
                    return ServiceResult.Success();

                await _objaveRepository.DeleteKomentarReakcijaById(exisitingKomentarReakcija.Id);
            }

            ObjavaKomentarReakcija komentarReakcija = new()
            {
                KorisnikId = korisnik.Id,
                KomentarId = komentarId,
                Liked = true,
            };

            await _objaveRepository.CreateKomentarReakcija(komentarReakcija);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DislikeObjavaKomentar(int komentarId)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            var exisitingKomentarReakcija = await _objaveRepository.GetKomentarReakcija(komentarId, korisnik.Id);

            if (exisitingKomentarReakcija != null)
            {
                if (exisitingKomentarReakcija.Liked == false)
                    return ServiceResult.Success();

                await _objaveRepository.DeleteKomentarReakcijaById(exisitingKomentarReakcija.Id);
            }

            ObjavaKomentarReakcija komentarReakcija = new()
            {
                KorisnikId = korisnik.Id,
                KomentarId = komentarId,
                Liked = false,
            };

            await _objaveRepository.CreateKomentarReakcija(komentarReakcija);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> ClearObjavaKomentarReaction(int komentarId)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            var komentar = await _objaveRepository.GetKomentarById(komentarId);

            if (komentar == null || !_ownershipService.Owns(komentar)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            await _objaveRepository.DeleteKomentarReakcija(komentarId, korisnik.Id);
            return ServiceResult.Success();
        }

        #endregion

    }
}
