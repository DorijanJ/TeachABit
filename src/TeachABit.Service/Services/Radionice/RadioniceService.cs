using AutoMapper;
using TeachABit.Model.DTOs.Radionice;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Model.Models.Radionice;
using TeachABit.Repository.Repositories.Radionice;

namespace TeachABit.Service.Services.Radionice;

public class RadioniceService(IRadioniceRepository radioniceRepository, IMapper mapper) : IRadioniceService
{
    private readonly IRadioniceRepository _radioniceRepository = radioniceRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<ServiceResult<List<RadionicaDto>>> GetRadionicaList(string? search = null)
    {
        var radionice = await _radioniceRepository.GetRadionicaList(search);
        var radioniceDto = _mapper.Map<List<RadionicaDto>>(radionice);
        return ServiceResult.Success(radioniceDto);
    }

    public async Task<ServiceResult<RadionicaDto>> GetRadionica(int id)
    {
        RadionicaDto? radionica = _mapper.Map<RadionicaDto>(await _radioniceRepository.GetRadionica(id));
        // Ovaj MessageDescriber još nije dodan u codebase
        if (radionica == null) return ServiceResult.Failure(MessageDescriber.ItemNotFound());
        return ServiceResult.Success(radionica);
    }

    public async Task<ServiceResult<RadionicaDto>> CreateRadionica(RadionicaDto radionica)
    {
        RadionicaDto createdRadionica = _mapper.Map<RadionicaDto>(await _radioniceRepository.CreateRadionica(_mapper.Map<Radionica>(radionica)));
        return ServiceResult.Success(createdRadionica);
    }
    /*public async Task<ServiceResult<RadionicaDto>> UpdateRadionica(RadionicaDto radionica)
    {
        // Moram provjeriti najbolji način implementacije za update.
        ...
    }*/


    public async Task<ServiceResult> DeleteRadionica(int id)
    {
        await _radioniceRepository.DeleteRadionica(id);
        return ServiceResult.Success();
    }
}