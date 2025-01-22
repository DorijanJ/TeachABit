using AutoMapper;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Model.DTOs.Tecajevi;
using TeachABit.Model.Enums;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Model.Models.Tecajevi;
using TeachABit.Repository.Repositories.Tecajevi;
using TeachABit.Service.Services.Authorization;
using TeachABit.Service.Util.Images;
using TeachABit.Service.Util.S3;

namespace TeachABit.Service.Services.Tecajevi
{
    public class TecajeviService(ITecajeviRepository tecajeviRepository, IMapper mapper, IImageManipulationService imageManipulation, IAuthorizationService authorizationService, IS3BucketService s3BucketService, IOwnershipService ownershipService) : ITecajeviService
    {
        private readonly ITecajeviRepository _tecajeviRepository = tecajeviRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IS3BucketService _bucketService = s3BucketService;
        private readonly IAuthorizationService _authorizationService = authorizationService;
        private readonly IImageManipulationService _imageManipulationService = imageManipulation;
        private readonly IOwnershipService _ownershipService = ownershipService;

        public async Task<ServiceResult<TecajDto>> GetTecaj(int id)
        {
            TecajDto? tecaj = _mapper.Map<TecajDto>(await _tecajeviRepository.GetTecaj(id));
            if (tecaj == null) return ServiceResult.Failure(MessageDescriber.ItemNotFound());

            var korisnik = _authorizationService.GetKorisnikOptional();

            if (tecaj.Cijena != null && (korisnik == null || !_ownershipService.Owns(tecaj)) && (korisnik == null || !await _tecajeviRepository.CheckIfTecajPlacen(korisnik.Id, tecaj.Id)))
                return ServiceResult.Failure(MessageDescriber.Unauthorized());

            return ServiceResult.Success(tecaj);
        }

        public async Task<ServiceResult<TecajDto>> CreateTecaj(CreateOrUpdateTecajDto tecaj)
        {
            if (tecaj.Cijena.HasValue)
            {
                tecaj.Cijena = Math.Round(tecaj.Cijena.Value, 2);
            }

            var korisnik = _authorizationService.GetKorisnik();
            tecaj.VlasnikId = korisnik.Id;

            Tecaj tecajModel = _mapper.Map<Tecaj>(tecaj);

            if (!string.IsNullOrEmpty(tecaj.NaslovnaSlikaBase64))
            {
                Guid naslovnaVersion = Guid.NewGuid();
                var resizedSlika = await _imageManipulationService.ConvertToNaslovnaSlika(tecaj.NaslovnaSlikaBase64);
                await _bucketService.UploadImageAsync(naslovnaVersion.ToString(), resizedSlika);
                tecajModel.NaslovnaSlikaVersion = naslovnaVersion.ToString();
            }

            TecajDto createdTecaj = _mapper.Map<TecajDto>(await _tecajeviRepository.CreateTecaj(tecajModel));
            return ServiceResult.Success(createdTecaj);
        }

        public async Task<ServiceResult> DeleteTecaj(int id)
        {
            Tecaj? tecaj = await _tecajeviRepository.GetTecaj(id);

            bool isModerator = await _authorizationService.HasPermission(LevelPristupaEnum.Moderator);

            if (tecaj == null || !(_ownershipService.Owns(tecaj) || isModerator)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            await _tecajeviRepository.DeleteTecaj(id);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<TecajDto>>> GetTecajList(string? search = null)
        {
            var korisnik = _authorizationService.GetKorisnikOptional();
            var tecajevi = await _tecajeviRepository.GetTecajList(search, korisnik?.Id);
            var tecajeviDto = _mapper.Map<List<TecajDto>>(tecajevi);
            return ServiceResult.Success(tecajeviDto);
        }

        public async Task<ServiceResult<List<LekcijaDto>>> GetLekcijaList(string? search = null)
        {
            var lekcije = await _tecajeviRepository.GetLekcijaList(search);
            var lekcijeDto = _mapper.Map<List<LekcijaDto>>(lekcije);
            return ServiceResult.Success(lekcijeDto);
        }

        public async Task<ServiceResult<TecajDto>> UpdateTecaj(CreateOrUpdateTecajDto updateTecaj)
        {
            if (!updateTecaj.Id.HasValue) return ServiceResult.Failure(MessageDescriber.ItemNotFound());
            var tecaj = await _tecajeviRepository.GetTecajByIdWithTracking(updateTecaj.Id.Value);

            if (tecaj == null || !_ownershipService.Owns(tecaj)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            if (!string.IsNullOrEmpty(updateTecaj.NaslovnaSlikaBase64))
            {
                Guid naslovnaVersion = Guid.NewGuid();
                var resizedSlika = await _imageManipulationService.ConvertToNaslovnaSlika(updateTecaj.NaslovnaSlikaBase64);
                await _bucketService.UploadImageAsync(naslovnaVersion.ToString(), resizedSlika);
                tecaj.NaslovnaSlikaVersion = naslovnaVersion.ToString();
            }

            tecaj.Naziv = updateTecaj.Naziv;
            tecaj.Opis = updateTecaj.Opis;
            tecaj.Cijena = updateTecaj.Cijena;

            var updatedTecaj = _mapper.Map<TecajDto>(await _tecajeviRepository.UpdateTecaj(tecaj));

            return ServiceResult.Success(updatedTecaj);
        }

        public async Task<ServiceResult<LekcijaDto>> CreateLekcija(LekcijaDto lekcijaDto, int id)
        {
            var tecaj = await _tecajeviRepository.GetTecaj(id);

            if (tecaj == null || !_ownershipService.Owns(tecaj)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            lekcijaDto.CreatedDateTime = DateTime.UtcNow;
            lekcijaDto.TecajId = id;

            LekcijaDto createdLekcija = _mapper.Map<LekcijaDto>(await _tecajeviRepository.CreateLekcija(_mapper.Map<Lekcija>(lekcijaDto)));
            return ServiceResult.Success(createdLekcija);
        }

        public async Task<ServiceResult> DeleteLekcija(int id)
        {
            Lekcija? lekcija = await _tecajeviRepository.GetLekcijaById(id);

            if (lekcija == null) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            var tecaj = await _tecajeviRepository.GetTecaj(lekcija.TecajId);

            if (lekcija == null || !_ownershipService.Owns(lekcija.Tecaj)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            await _tecajeviRepository.DeleteLekcija(id, false);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<LekcijaDto>> UpdateLekcija(UpdateLekcijaDto updateLekcija)
        {
            var lekcija = await _tecajeviRepository.GetLekcijaByIdWithTracking(updateLekcija.Id);

            if (lekcija == null) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            var tecaj = await _tecajeviRepository.GetTecaj(lekcija.TecajId);

            if (tecaj == null || !_ownershipService.Owns(tecaj)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            lekcija.Naziv = updateLekcija.Naziv;
            lekcija.Sadrzaj = updateLekcija.Sadrzaj;
            lekcija.LastUpdatedDateTime = DateTime.UtcNow;

            var updatedLekcija = _mapper.Map<LekcijaDto>(await _tecajeviRepository.UpdateLekcija(lekcija));

            return ServiceResult.Success(updatedLekcija);
        }

        public async Task<ServiceResult<TecajKomentarDto>> CreateKomentarTecaj(TecajKomentarDto komentarTecaj, int tecajId)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            komentarTecaj.TecajId = tecajId;
            komentarTecaj.VlasnikId = korisnik.Id;
            komentarTecaj.CreatedDateTime = DateTime.UtcNow;

            TecajKomentarDto createdKomentar = _mapper.Map<TecajKomentarDto>(await _tecajeviRepository.CreateKomentarTecaj(_mapper.Map<TecajKomentar>(komentarTecaj)));
            return ServiceResult.Success(createdKomentar);
        }

        public async Task<ServiceResult<List<TecajKomentarDto>>> GetKomentarTecajListRecursive(int id, int? nadKomentarId = null)
        {
            List<TecajKomentarDto> komentari = _mapper.Map<List<TecajKomentarDto>>(await _tecajeviRepository.GetPodKomentarTecajList(id, nadKomentarId));
            Korisnik? korisnik = _authorizationService.GetKorisnikOptional();

            foreach (var komentar in komentari)
            {
                if (korisnik != null)
                {
                    komentar.Liked = (await _tecajeviRepository.GetKomentarTecajReakcija(komentar.Id, korisnik.Id))?.Liked;
                }
                komentar.PodKomentarList = (await GetKomentarTecajListRecursive(id, komentar.Id)).Data;
            }

            return ServiceResult.Success(komentari);
        }

        public async Task<ServiceResult> DeleteKomentarTecaj(int id)
        {
            TecajKomentar? komentar = await _tecajeviRepository.GetKomentarTecajById(id);

            bool isModerator = await _authorizationService.HasPermission(LevelPristupaEnum.Moderator);

            if (komentar == null || !(_ownershipService.Owns(komentar) || isModerator)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            if (komentar.IsDeleted) return ServiceResult.Failure(MessageDescriber.BadRequest("Komentar je već izbrisan."));

            var hasPodkomentari = await _tecajeviRepository.HasPodkomentari(id);

            await _tecajeviRepository.DeleteKomentarTecaj(id, keepEntry: hasPodkomentari);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> LikeKomentarTecaj(int id)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            var exisitingKomentarReakcija = await _tecajeviRepository.GetKomentarTecajReakcija(id, korisnik.Id);

            if (exisitingKomentarReakcija != null)
            {
                if (exisitingKomentarReakcija.Liked == true)
                    return ServiceResult.Success();

                await _tecajeviRepository.DeleteKomentarTecajReakcija(exisitingKomentarReakcija.Id);
            }
            KomentarTecajReakcija komentarReakcija = new()
            {
                KorisnikId = korisnik.Id,
                KomentarId = id,
                Liked = true,
            };

            await _tecajeviRepository.CreateKomentarTecajReakcija(komentarReakcija);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> DislikeKomentarTecaj(int id)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            var exisitingKomentarReakcija = await _tecajeviRepository.GetKomentarTecajReakcija(id, korisnik.Id);

            if (exisitingKomentarReakcija != null)
            {
                if (exisitingKomentarReakcija.Liked == false)
                    return ServiceResult.Success();

                await _tecajeviRepository.DeleteKomentarTecajReakcija(exisitingKomentarReakcija.Id);
            }

            KomentarTecajReakcija komentarReakcija = new()
            {
                KorisnikId = korisnik.Id,
                KomentarId = id,
                Liked = false,
            };

            await _tecajeviRepository.CreateKomentarTecajReakcija(komentarReakcija);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult> ClearKomentarTecajReaction(int id)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();
            await _tecajeviRepository.DeleteKomentarTecajReakcija(id, korisnik.Id);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<TecajKomentarDto>> UpdateKomentarTecaj(UpdateKomentarTecajDto updateKomentar)
        {
            var komentar = await _tecajeviRepository.GetKomentarTecajByIdWithTracking(updateKomentar.Id);

            if (komentar == null || !_ownershipService.Owns(komentar) || komentar.IsDeleted) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            komentar.Sadrzaj = updateKomentar.Sadrzaj;
            komentar.LastUpdatedDateTime = DateTime.UtcNow;

            var updatedKomentar = _mapper.Map<TecajKomentarDto>(await _tecajeviRepository.UpdateKomentarTecaj(komentar));

            return ServiceResult.Success(updatedKomentar);
        }
    }
}