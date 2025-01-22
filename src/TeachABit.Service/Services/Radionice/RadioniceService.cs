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
        radionica.MaksimalniKapacitet = updateRadionica.MaksimalniKapacitet;
        radionica.VrijemeRadionice = updateRadionica.VrijemeRadionice;

        var updatedRadionica = _mapper.Map<RadionicaDto>(await _radioniceRepository.UpdateRadionica(radionica));

        return ServiceResult.Success(updatedRadionica);
    }
    
    public async Task<ServiceResult> DeleteRadionica(int id)
    {
        Korisnik korisnik = _authorizationService.GetKorisnik();

        RadionicaDto? objava = _mapper.Map<RadionicaDto?>(await _radioniceRepository.GetRadionicaById(id));

        bool isAdmin = await _authorizationService.IsAdmin();

        if (!isAdmin && (objava == null || !korisnik.Owns(objava))) return ServiceResult.Failure(MessageDescriber.Unauthorized());

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
            if (korisnik != null)
            {
                komentar.Liked = (await _radioniceRepository.GetKomentarRadionicaReakcija(komentar.Id, korisnik.Id))?.Liked;
            }
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
    
    public async Task<ServiceResult> LikeRadionicaKomentar(int id)
    {
        Korisnik korisnik = _authorizationService.GetKorisnik();

        var exisitingKomentarReakcija = await _radioniceRepository.GetKomentarRadionicaReakcija(id, korisnik.Id);

        if (exisitingKomentarReakcija != null)
        {
            if (exisitingKomentarReakcija.Liked == true)
                return ServiceResult.Success();

            await _radioniceRepository.DeleteKomentarReakcija(exisitingKomentarReakcija.Id);
        }

        KomentarRadionicaReakcija komentarReakcija = new()
        {
            KorisnikId = korisnik.Id,
            KomentarId = id,
            Liked = true,
        };

        await _radioniceRepository.CreateKomentarReakcija(komentarReakcija);
        return ServiceResult.Success();
    }
    
    public async Task<ServiceResult> DislikeRadionicaKomentar(int id)
    {
        Korisnik korisnik = _authorizationService.GetKorisnik();

        var exisitingKomentarReakcija = await _radioniceRepository.GetKomentarRadionicaReakcija(id, korisnik.Id);

        if (exisitingKomentarReakcija != null)
        {
            if (exisitingKomentarReakcija.Liked == false)
                return ServiceResult.Success();

            await _radioniceRepository.DeleteKomentarReakcija(exisitingKomentarReakcija.Id);
        }

        KomentarRadionicaReakcija komentarRadionicaReakcija = new()
        {
            KorisnikId = korisnik.Id,
            KomentarId = id,
            Liked = false,
        };

        await _radioniceRepository.CreateKomentarReakcija(komentarRadionicaReakcija);
        return ServiceResult.Success();
    }
    
    public async Task<ServiceResult> ClearKomentarReaction(int id)
    {
        Korisnik korisnik = _authorizationService.GetKorisnik();
        await _radioniceRepository.DeleteKomentarReakcija(id, korisnik.Id);
        return ServiceResult.Success();
    }
    public async Task<ServiceResult<List<RadionicaDto>>> GetAllRadioniceForCurrentUser(string username)
    {
        var korisnik = _authorizationService.GetKorisnikOptional();
        var radionice = await _radioniceRepository.GetAllRadioniceForCurrentUser(username);
        var radioniceDto = _mapper.Map<List<RadionicaDto>>(radionice);
        return ServiceResult.Success(radioniceDto);
    }
    public async Task<ServiceResult<List<RadionicaDto>>> GetAllRadionicefavoritForCurrentUser(string username)
    {
        var korisnik = _authorizationService.GetKorisnikOptional();
        var radionice = await _radioniceRepository.GetAllRadioniceFavoritForCurrentUser(username);
        var radioniceDto = _mapper.Map<List<RadionicaDto>>(radionice);
        return ServiceResult.Success(radioniceDto);
    }
}