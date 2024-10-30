using AutoMapper;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Model.DTOs.Tecaj;
using TeachABit.Repository.Repositories.Tecaj;

namespace TeachABit.Service.Services.Tecaj;

public class TecajeviService(ITecajeviRepository tecajeviRepository, IMapper mapper) : ITecajeviService
{
    private readonly ITecajeviRepository _TecajeviRepository = tecajeviRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ServiceResult<List<TecajDto>>> GetTecajList()
    {
        List<TecajDto> Tecajevi = _mapper.Map<List<TecajDto>>(await _TecajeviRepository.GetTecajList());
        return ServiceResult<List<TecajDto>>.Success(Tecajevi);
    }
    public async Task<ServiceResult<TecajDto>> GetTecaj(int id)
    {
        TecajDto? Tecaj = _mapper.Map<TecajDto>(await _TecajeviRepository.GetTecaj(id));
        if(Tecaj == null) return ServiceResult<TecajDto>.Failure(MessageDescriber.ItemNotFound());
        return ServiceResult<TecajDto>.Success(Tecaj);
    }
    public async Task<ServiceResult<TecajDto>> CreateTecaj(TecajDto Tecaj)
    {
        TecajDto createdTecaj = _mapper.Map<TecajDto>(await _TecajeviRepository.CreateTecaj(_mapper.Map<Model.Models.Tecaj.Tecaj>(Tecaj)));
        return ServiceResult<TecajDto>.Success(createdTecaj);
    }
    /*public async Task<ServiceResult<TecajDto>> UpdateTecaj(TecajDto Tecaj)
    {
        // Moram provjeriti najbolji način implementacije za update.
        ...
    }*/
    public async Task<ServiceResult> DeleteTecaj(int id)
    {
        await _TecajeviRepository.DeleteTecaj(id);
        return ServiceResult.Success();
    }
}