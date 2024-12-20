using AutoMapper;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Model.DTOs.Tecajevi;
using TeachABit.Model.Models.Tecajevi;
using TeachABit.Repository.Repositories.Tecajevi;


namespace TeachABit.Service.Services.Tecajevi
{
    public class TecajeviService(ITecajeviRepository tecajeviRepository, IMapper mapper) : ITecajeviService
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
            if (tecaj == null) return ServiceResult<TecajDto>.Failure(MessageDescriber.ItemNotFound());
            return ServiceResult<TecajDto>.Success(tecaj);
        }

        public async Task<ServiceResult<TecajDto>> CreateTecaj(TecajDto tecaj)
        {
            TecajDto createdTecaj = _mapper.Map<TecajDto>(await _tecajeviRepository.CreateTecaj(_mapper.Map<Tecaj>(tecaj)));
            return ServiceResult<TecajDto>.Success(createdTecaj);
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
            return ServiceResult<List<TecajDto>>.Success(tecajeviDto);
        }


    }
}