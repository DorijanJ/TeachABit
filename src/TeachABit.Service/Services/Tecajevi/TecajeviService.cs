using AutoMapper;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Model.DTOs.Tecajevi;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Model.Models.Korisnici.Extensions;
using TeachABit.Model.Models.Tecajevi;
using TeachABit.Repository.Repositories.Tecajevi;
using TeachABit.Service.Services.Authorization;
using TeachABit.Service.Util.Images;
using TeachABit.Service.Util.S3;

namespace TeachABit.Service.Services.Tecajevi
{
    public class TecajeviService(ITecajeviRepository tecajeviRepository, IMapper mapper, IImageManipulationService imageManipulation, IAuthorizationService authorizationService, IS3BucketService s3BucketService) : ITecajeviService
    {
        private readonly ITecajeviRepository _tecajeviRepository = tecajeviRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IS3BucketService _bucketService = s3BucketService;
        private readonly IAuthorizationService _authorizationService = authorizationService;
        private readonly IImageManipulationService _imageManipulationService = imageManipulation;

        /*public async Task<ServiceResult<List<TecajDto>>> GetTecajList()
        {
            List<TecajDto> tecajevi = _mapper.Map<List<TecajDto>>(await _tecajeviRepository.GetTecajList());
            return ServiceResult<List<TecajDto>>.Success(tecajevi);
        }*/
        public async Task<ServiceResult<TecajDto>> GetTecaj(int id)
        {
            TecajDto? tecaj = _mapper.Map<TecajDto>(await _tecajeviRepository.GetTecaj(id));
            if (tecaj == null) return ServiceResult.Failure(MessageDescriber.ItemNotFound());

            Korisnik? korisnik = _authorizationService.GetKorisnikOptional();

            if (tecaj.Cijena != null)
            {
                if (korisnik == null)
                    return ServiceResult.Failure(MessageDescriber.Unauthorized());
                else
                {
                    var tecajPlacen = await _tecajeviRepository.CheckIfTecajPlacen(korisnik.Id, tecaj.Id);
                    if (!tecajPlacen) return ServiceResult.Failure(MessageDescriber.Unauthorized());
                }
            }

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
        public async Task<ServiceResult<List<TecajDto>>> GetTecajListByFiltratingOcjena(int ocjena)
        {
            //var korisnik = _authorizationService.GetKorisnikOptional();
            var tecajevi = await _tecajeviRepository.GetTecajListByFiltratingOcjena(ocjena);
            var tecajeviDto = _mapper.Map<List<TecajDto>>(tecajevi);
            return ServiceResult.Success(tecajeviDto);
        }
        public async Task<ServiceResult<List<TecajDto>>> GetTecajListByFiltratingCijena(int maxCijena, int minCijena)
        {
            if (maxCijena < minCijena) return ServiceResult.Failure(MessageDescriber.BadRequest("Minimala cijena ne može biti veća od maksmimalne cijene"));
            var korisnik = _authorizationService.GetKorisnikOptional();
            var tecajevi = await _tecajeviRepository.GetTecajListByFiltratingCijena(maxCijena, minCijena, korisnik?.Id);
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
            var user = _authorizationService.GetKorisnik();

            if (tecaj == null || !user.Owns(tecaj)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            tecaj.Naziv = updateTecaj.Naziv;
            tecaj.Opis = updateTecaj.Opis;
            tecaj.Cijena = updateTecaj.Cijena;

            var updatedTecaj = _mapper.Map<TecajDto>(await _tecajeviRepository.UpdateTecaj(tecaj));

            return ServiceResult.Success(updatedTecaj);
        }

        public async Task<ServiceResult<LekcijaDto>> CreateLekcija(LekcijaDto lekcijaDto, int id)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            var tecaj = await _tecajeviRepository.GetTecajByIdWithTracking(id);

            if (tecaj == null || !korisnik.Owns(tecaj)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            lekcijaDto.CreatedDateTime = DateTime.UtcNow;
            lekcijaDto.TecajId = id;

            LekcijaDto createdLekcija = _mapper.Map<LekcijaDto>(await _tecajeviRepository.CreateLekcija(_mapper.Map<Lekcija>(lekcijaDto)));
            return ServiceResult.Success(createdLekcija);
        }




        public async Task<ServiceResult> DeleteLekcija(int id)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            Lekcija? lekcija = await _tecajeviRepository.GetLekcijaById(id);

            bool isAdmin = await _authorizationService.IsAdmin();

            if (lekcija == null || (!isAdmin && !korisnik.Owns(lekcija.Tecaj))) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            await _tecajeviRepository.DeleteLekcija(id, false);
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<LekcijaDto>> UpdateLekcija(UpdatedLekcijaDto updateLekcija)
        {
            var lekcija = await _tecajeviRepository.GetLekcijaByIdWithTracking(updateLekcija.Id);
            var user = _authorizationService.GetKorisnik();

            if (lekcija == null || !user.Owns(lekcija.Tecaj)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            lekcija.Sadrzaj = updateLekcija.Sadrzaj;
            lekcija.LastUpdatedDateTime = DateTime.UtcNow;

            var updatedLekcija = _mapper.Map<LekcijaDto>(await _tecajeviRepository.UpdateLekcija(lekcija));

            return ServiceResult.Success(updatedLekcija);
        }

        public async Task<ServiceResult<KomentarTecajDto>> CreateKomentarTecaj(KomentarTecajDto komentarTecaj, int tecajId)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            komentarTecaj.TecajId = tecajId;
            komentarTecaj.VlasnikId = korisnik.Id;
            komentarTecaj.CreatedDateTime = DateTime.UtcNow;

            KomentarTecajDto createdKomentar = _mapper.Map<KomentarTecajDto>(await _tecajeviRepository.CreateKomentarTecaj(_mapper.Map<KomentarTecaj>(komentarTecaj)));
            return ServiceResult.Success(createdKomentar);
        }

        public async Task<ServiceResult<List<KomentarTecajDto>>> GetKomentarTecajListRecursive(int id, int? nadKomentarId = null)
        {
            List<KomentarTecajDto> komentari = _mapper.Map<List<KomentarTecajDto>>(await _tecajeviRepository.GetPodKomentarTecajList(id, nadKomentarId));
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
            Korisnik korisnik = _authorizationService.GetKorisnik();

            KomentarTecaj? komentar = await _tecajeviRepository.GetKomentarTecajById(id);

            bool isAdmin = await _authorizationService.IsAdmin();

            if (komentar == null || (!isAdmin && !korisnik.Owns(komentar))) return ServiceResult.Failure(MessageDescriber.Unauthorized());

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

        public async Task<ServiceResult<KomentarTecajDto>> UpdateKomentarTecaj(UpdateKomentarTecajDto updateKomentar)
        {
            var komentar = await _tecajeviRepository.GetKomentarTecajByIdWithTracking(updateKomentar.Id);
            var user = _authorizationService.GetKorisnik();

            if (komentar == null || !user.Owns(komentar) || komentar.IsDeleted) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            komentar.Sadrzaj = updateKomentar.Sadrzaj;
            komentar.LastUpdatedDateTime = DateTime.UtcNow;

            var updatedKomentar = _mapper.Map<KomentarTecajDto>(await _tecajeviRepository.UpdateKomentarTecaj(komentar));

            return ServiceResult.Success(updatedKomentar);
        }
    }
}