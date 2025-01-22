using AutoMapper;
using TeachABit.Model.DTOs.Radionice;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Model.Models.Korisnici.Extensions;
using TeachABit.Model.Models.Radionice;
using TeachABit.Repository.Repositories.Radionice;
using TeachABit.Service.Services.Authorization;

namespace TeachABit.Service.Services.Radionice;

public class RadioniceService(IRadioniceRepository radioniceRepository, IMapper mapper, IAuthorizationService authorizationService) : IRadioniceService
{
    private readonly IRadioniceRepository _radioniceRepository = radioniceRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IAuthorizationService _authorizationService = authorizationService;

    public async Task<ServiceResult<List<RadionicaDto>>> GetRadionicaList(string? search = null)
    {
        var radionice = await _radioniceRepository.GetRadionicaList(search);
        var radioniceDto = _mapper.Map<List<RadionicaDto>>(radionice);
        return ServiceResult.Success(radioniceDto);
    }

    public async Task<ServiceResult<RadionicaDto>> GetRadionica(int id)
    {
        RadionicaDto? radionica = _mapper.Map<RadionicaDto>(await _radioniceRepository.GetRadionica(id));
        if (radionica == null) return ServiceResult.Failure(MessageDescriber.ItemNotFound());
        return ServiceResult.Success(radionica);
    }

    public async Task<ServiceResult<RadionicaDto>> CreateRadionica(RadionicaDto radionica)
    {
        var korisnik = _authorizationService.GetKorisnik();

        radionica.VlasnikId = korisnik.Id;

        RadionicaDto createdRadionica = _mapper.Map<RadionicaDto>(await _radioniceRepository.CreateRadionica(_mapper.Map<Radionica>(radionica)));
        return ServiceResult.Success(createdRadionica);
    }
    public async Task<ServiceResult<RadionicaDto>> UpdateRadionica(UpdateRadionicaDto updateRadionica)
    {
        var radionica = await _radioniceRepository.GetRadionicaByIdWithTracking(updateRadionica.Id);
        var user = _authorizationService.GetKorisnik();

        if (radionica == null || !user.Owns(radionica)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

        radionica.Naziv = updateRadionica.Naziv;
        radionica.Opis = updateRadionica.Opis;
        radionica.Cijena = updateRadionica.Cijena;

        var updatedTecaj = _mapper.Map<RadionicaDto>(await _radioniceRepository.UpdateRadionica(radionica));

        return ServiceResult.Success(updatedTecaj);
    }


    public async Task<ServiceResult> DeleteRadionica(int id)
    {
        await _radioniceRepository.DeleteRadionica(id);
        return ServiceResult.Success();
    }

    public async Task<ServiceResult<KomentarRadionicaDto>> CreateKomentar(KomentarRadionicaDto komentar, int radionicaId)
    {
        Korisnik korisnik = _authorizationService.GetKorisnik();

        komentar.VlasnikId = korisnik.Id;
        komentar.CreatedDateTime = DateTime.UtcNow;
        komentar.RadionicaId = radionicaId;

        KomentarRadionicaDto createdKomentar = _mapper.Map<KomentarRadionicaDto>(await _radioniceRepository.CreateKomentar(_mapper.Map<KomentarRadionica>(komentar)));
        return ServiceResult.Success(createdKomentar);
    }

    public async Task<ServiceResult> DeleteKomentar(int id)
    {
        Korisnik korisnik = _authorizationService.GetKorisnik();

        KomentarRadionica? komentar = await _radioniceRepository.GetKomentarById(id);

        bool isAdmin = await _authorizationService.IsAdmin();

        if (komentar == null || (!isAdmin && !korisnik.Owns(komentar))) return ServiceResult.Failure(MessageDescriber.Unauthorized());

        if (komentar.IsDeleted) return ServiceResult.Failure(MessageDescriber.BadRequest("Komentar je već izbrisan."));

        var hasPodkomentari = await _radioniceRepository.HasPodkomentari(id);

        await _radioniceRepository.DeleteKomentar(id, keepEntry: hasPodkomentari);
        return ServiceResult.Success();
    }

    public async Task<ServiceResult<List<KomentarRadionicaDto>>> GetKomentarListRecursive(int id, int? nadKomentarId = null)
    {
        List<KomentarRadionicaDto> komentari = _mapper.Map<List<KomentarRadionicaDto>>(await _radioniceRepository.GetPodKomentarList(id, nadKomentarId));
        Korisnik? korisnik = _authorizationService.GetKorisnikOptional();

        foreach (var komentar in komentari)
        {
            /*if (korisnik != null)
            {
                komentar.Liked = (await _radioniceRepository.GetKomentarReakcija(komentar.Id, korisnik.Id))?.Liked;
            }*/
            //dodati ovo iznad kad ce se dodavati reakcije na komentar
            komentar.PodKomentarList = _mapper.Map<List<KomentarRadionicaDto>>((await GetKomentarListRecursive(id, komentar.Id)).Data);
        }

        return ServiceResult.Success(komentari);
    }

    public async Task<ServiceResult<KomentarRadionicaDto>> UpdateKomentar(UpdateKomentarRadionicaDto updateKomentar)
    {
        var komentar = await _radioniceRepository.GetKomentarRadionicaByIdWithTracking(updateKomentar.Id);
        var user = _authorizationService.GetKorisnik();

        if (komentar == null || !user.Owns(komentar) || komentar.IsDeleted) return ServiceResult.Failure(MessageDescriber.Unauthorized());

        komentar.Sadrzaj = updateKomentar.Sadrzaj;
        komentar.LastUpdatedDateTime = DateTime.UtcNow;

        var updatedKomentar = _mapper.Map<KomentarRadionicaDto>(await _radioniceRepository.UpdateKomentar(komentar));

        return ServiceResult.Success(updatedKomentar);

    }
    
    public async Task<ServiceResult<RadionicaOcjena?>> GetOcjena(int radionicaId, string korisnikId)
    {
        var ocjena = await _radioniceRepository.GetOcjena(radionicaId, korisnikId);
        RadionicaOcjena? ocjenaRad = _mapper.Map<RadionicaOcjena>(ocjena);

        if (ocjenaRad == null) 
            return ServiceResult.Failure(MessageDescriber.ItemNotFound());

        return ServiceResult.Success(ocjenaRad);
    }
    
    public async Task<ServiceResult<RadionicaOcjena>> CreateOcjena(int radionicaId, string korisnikId, double ocjena)
    {
        var novaOcjena = new RadionicaOcjena
        {
            RadionicaId = radionicaId,
            KorisnikId = korisnikId,
            Ocjena = ocjena
        };

        var createdOcjena = await _radioniceRepository.CreateOcjena(novaOcjena);
        var ocjenaDto = _mapper.Map<RadionicaOcjena>(createdOcjena);
        return ServiceResult.Success(ocjenaDto);
    }
    
    public async Task<ServiceResult<RadionicaOcjena>> UpdateOcjena(int radionicaId, string korisnikId, double ocjena)
    {
        var postojećaOcjena = await _radioniceRepository.GetOcjena(radionicaId, korisnikId);
        var user = _authorizationService.GetKorisnik();

        if (postojećaOcjena == null || user.Id != korisnikId) 
            return ServiceResult.Failure(MessageDescriber.Unauthorized());

        postojećaOcjena.Ocjena = ocjena;

        var updatedOcjena = _mapper.Map<RadionicaOcjena>(await _radioniceRepository.UpdateOcjena(postojećaOcjena));

        return ServiceResult.Success(updatedOcjena);
    }
    
    public async Task<ServiceResult> DeleteOcjena(int radionicaId, string korisnikId)
    {
        await _radioniceRepository.DeleteOcjena(radionicaId, korisnikId);
        return ServiceResult.Success();
    }
    
    
    
}