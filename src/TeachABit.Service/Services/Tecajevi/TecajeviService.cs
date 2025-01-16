using AutoMapper;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Model.DTOs.Tecajevi;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Model.Models.Korisnici.Extensions;
using TeachABit.Model.Models.Tecajevi;
using TeachABit.Repository.Repositories.Tecajevi;
using TeachABit.Service.Services.Authorization;

namespace TeachABit.Service.Services.Tecajevi
{
    public class TecajeviService(ITecajeviRepository tecajeviRepository, IMapper mapper, IAuthorizationService authorizationService) : ITecajeviService
    {
        private readonly ITecajeviRepository _tecajeviRepository = tecajeviRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IAuthorizationService _authorizationService = authorizationService;

        /*public async Task<ServiceResult<List<TecajDto>>> GetTecajList()
        {
            List<TecajDto> tecajevi = _mapper.Map<List<TecajDto>>(await _tecajeviRepository.GetTecajList());
            return ServiceResult<List<TecajDto>>.Success(tecajevi);
        }*/
        public async Task<ServiceResult<TecajDto>> GetTecaj(int id)
        {
            TecajDto? tecaj = _mapper.Map<TecajDto>(await _tecajeviRepository.GetTecaj(id));
            if (tecaj == null) return ServiceResult.Failure(MessageDescriber.ItemNotFound());

            var korisnik = _authorizationService.GetKorisnikOptional();

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

        public async Task<ServiceResult<TecajDto>> CreateTecaj(TecajDto tecaj)
        {
            if (tecaj.Cijena.HasValue)
            {
                tecaj.Cijena = Math.Round(tecaj.Cijena.Value, 2);
            }

            TecajDto createdTecaj = _mapper.Map<TecajDto>(await _tecajeviRepository.CreateTecaj(_mapper.Map<Tecaj>(tecaj)));
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




        public async Task<ServiceResult<TecajDto>> UpdateTecaj(UpdateTecajDto updateTecaj)
        {
            var tecaj = await _tecajeviRepository.GetTecajByIdWithTracking(updateTecaj.Id);
            var user = _authorizationService.GetKorisnik();

            if (tecaj == null || !user.Owns(tecaj)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            tecaj.Naziv = updateTecaj.Naziv;
            tecaj.Sadrzaj = updateTecaj.Sadrzaj;
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

            komentarTecaj.VlasnikId = korisnik.Id;
            komentarTecaj.CreatedDateTime = DateTime.UtcNow;
            komentarTecaj.ObjavaId = tecajId;

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
                komentar.PodKomentarList = _mapper.Map<List<KomentarTecajDto>>((await GetKomentarTecajListRecursive(id, komentar.Id)).Data);
            }

            return ServiceResult.Success(komentari);
        }

        public async Task<ServiceResult> DeleteKomentarTecaj(int id)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();

            KomentarTecaj? komentar = await _tecajeviRepository.GetKomentarTecajById(id);

            bool isAdmin = await _authorizationService.IsAdmin();

            if (komentar == null || (!isAdmin && !korisnik.Owns(komentar.Tecaj))) return ServiceResult.Failure(MessageDescriber.Unauthorized());

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

            if (komentar == null || !user.Owns(komentar.Tecaj) || komentar.IsDeleted) return ServiceResult.Failure(MessageDescriber.Unauthorized());

            komentar.Sadrzaj = updateKomentar.Sadrzaj;
            komentar.LastUpdatedDateTime = DateTime.UtcNow;

            var updatedKomentar = _mapper.Map<KomentarTecajDto>(await _tecajeviRepository.UpdateKomentarTecaj(komentar));

            return ServiceResult.Success(updatedKomentar);
        }
    }
}