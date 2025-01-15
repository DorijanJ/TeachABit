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
            return ServiceResult.Success(tecaj);
        }

        public async Task<ServiceResult<TecajDto>> CreateTecaj(TecajDto tecaj)
        {
            TecajDto createdTecaj = _mapper.Map<TecajDto>(await _tecajeviRepository.CreateTecaj(_mapper.Map<Tecaj>(tecaj)));
            return ServiceResult.Success(createdTecaj);
        }
        /*public async Task<ServiceResult<TecajDto>> UpdateTecaj(TecajDto tecaj)
        {
            // Moram provjeriti najbolji način implementacije za update.
            ...
        }*/
        public async Task<ServiceResult> DeleteTecaj(int id)
        {
            await _tecajeviRepository.DeleteTecaj(id);
            return ServiceResult.Success();
        }
        public async Task<ServiceResult<List<TecajDto>>> GetTecajList(string? search = null)
        {
            var tecajevi = await _tecajeviRepository.GetTecajList(search);
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
    }
}