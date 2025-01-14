using AutoMapper;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Model.DTOs.Tecajevi;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Model.Models.Objave;
using TeachABit.Model.Models.Tecajevi;
using TeachABit.Repository.Repositories.Tecajevi;
using TeachABit.Service.Services.Tecajevi;
using TeachABit.Service.Services.Authorization;
using TeachABit.Model.Models.Korisnici.Extensions;

namespace TeachABit.Service.Services.Tecajevi
{
    public class TecajeviService(ITecajeviRepository tecajeviRepository, IMapper mapper, IAuthorizationService authorizationService) : ITecajeviService
    {
        private readonly ITecajeviRepository _tecajeviRepository = tecajeviRepository;
        private readonly IMapper _mapper = mapper;

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
        public async Task<ServiceResult<TecajDto>> UpdateTecaj(UpdateTecajDto updateObjava)
        {
            var tecaj = await _tecajeviRepository.GetTecajByIdWithTracking(updateObjava.Id);
            var user = _authorizationService.GetKorisnik();

            if (tecaj == null || !user.Owns(tecaj)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

        public async Task<ServiceResult<LekcijaDto>> CreateLekcija(LekcijaDto lekcijaDto, int id)
        {
            Korisnik korisnik = _authorizationService.GetKorisnik();
            
            lekcijaDto.CreatedDateTime = DateTime.UtcNow;
            lekcijaDto.Id = id;
            tecaj.Naziv = updateObjava.Naziv;
            tecaj.Sadrzaj = updateObjava.Sadrzaj;

            var updatedObjava = _mapper.Map<TecajDto>(await _tecajeviRepository.UpdateTecaj(tecaj));

            return ServiceResult.Success(updatedObjava);
        }

            KomentarDto createdLekcija = _mapper.Map<KomentarDto>(await _tecajeviRepository.CreateLekcija(_mapper.Map<Lekcija>(lekcijaDto)));
            return ServiceResult.Success(createdLekcija);
        }

        public Task<ServiceResult> DeleteLekcija(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<LekcijaDto>> UpdateLekcija(UpdatedLekcijaDto updateLekcija)
        {
            throw new NotImplementedException();
        }
    }
}