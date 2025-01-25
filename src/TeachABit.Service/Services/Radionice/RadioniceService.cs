using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Net.Mail;
using TeachABit.Model.DTOs.Korisnici;
using TeachABit.Model.DTOs.Radionice;
using TeachABit.Model.DTOs.Result;
using TeachABit.Model.DTOs.Result.Message;
using TeachABit.Model.Enums;
using TeachABit.Model.Models.Korisnici;
using TeachABit.Model.Models.Radionice;
using TeachABit.Repository.Repositories.Radionice;
using TeachABit.Service.Services.Authorization;
using TeachABit.Service.Util.Images;
using TeachABit.Service.Util.Mail;
using TeachABit.Service.Util.S3;

namespace TeachABit.Service.Services.Radionice;

public class RadioniceService(IRadioniceRepository radioniceRepository, UserManager<Korisnik> userManager, IS3BucketService bucketService, IImageManipulationService imageManipulationService, IMapper mapper, IAuthorizationService authorizationService, IOwnershipService ownershipService, IMailSenderService mailSenderService) : IRadioniceService
{
    private readonly IRadioniceRepository _radioniceRepository = radioniceRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IAuthorizationService _authorizationService = authorizationService;
    private readonly IOwnershipService _ownershipService = ownershipService;
    private readonly IImageManipulationService _imageManipulationService = imageManipulationService;
    private readonly IS3BucketService _bucketService = bucketService;
    private readonly UserManager<Korisnik> _userManager = userManager;
    private readonly IMailSenderService _mailSenderService = mailSenderService;

    public async Task<ServiceResult<List<RadionicaDto>>> GetRadionicaList(string? search = null, string? vlasnikUsername = null, double? minOcjena = null,
        double? maxOcjena = null, decimal? minCijena = null, decimal? maxCijena = null, bool sortOrderAsc = true, bool samoNadolazece = true)
    {
        var korisnik = _authorizationService.GetKorisnikOptional();

        string? vlasnikId = null;
        if (!string.IsNullOrEmpty(vlasnikUsername))
        {
            vlasnikId = (await _userManager.FindByNameAsync(vlasnikUsername))?.Id;
        }

        var radionice = await _radioniceRepository.GetRadionicaList(search, korisnik?.Id, vlasnikId, minOcjena, maxOcjena, minCijena, maxCijena, sortOrderAsc, samoNadolazece);
        var radioniceDto = _mapper.Map<List<RadionicaDto>>(radionice);
        return ServiceResult.Success(radioniceDto);
    }

    public async Task<ServiceResult<RadionicaDto>> GetRadionica(int id)
    {
        var korisnik = _authorizationService.GetKorisnikOptional();
        RadionicaDto? radionica = _mapper.Map<RadionicaDto>(await _radioniceRepository.GetRadionica(id, korisnik?.Id));
        if (radionica == null) return ServiceResult.Failure(MessageDescriber.ItemNotFound());
        return ServiceResult.Success(radionica);
    }

    public async Task<ServiceResult<RadionicaDto>> CreateRadionica(CreateOrUpdateRadionicaDto radionica)
    {
        var korisnik = _authorizationService.GetKorisnik();
        radionica.VlasnikId = korisnik.Id;

        Radionica radionicaModel = _mapper.Map<Radionica>(radionica);

        if (!string.IsNullOrEmpty(radionica.NaslovnaSlikaBase64))
        {
            Guid naslovnaVersion = Guid.NewGuid();
            var resizedSlika = await _imageManipulationService.ConvertToNaslovnaSlika(radionica.NaslovnaSlikaBase64);
            await _bucketService.UploadImageAsync(naslovnaVersion.ToString(), resizedSlika);
            radionicaModel.NaslovnaSlikaVersion = naslovnaVersion.ToString();
        }

        RadionicaDto createdRadionica = _mapper.Map<RadionicaDto>(await _radioniceRepository.CreateRadionica(radionicaModel));
        return ServiceResult.Success(createdRadionica);
    }
    public async Task<ServiceResult<RadionicaDto>> UpdateRadionica(CreateOrUpdateRadionicaDto updateRadionica)
    {
        if (!updateRadionica.Id.HasValue) return ServiceResult.Failure(MessageDescriber.ItemNotFound());
        var radionica = await _radioniceRepository.GetRadionicaByIdWithTracking(updateRadionica.Id.Value);

        if (radionica == null || !_ownershipService.Owns(radionica)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

        if (!string.IsNullOrEmpty(updateRadionica.NaslovnaSlikaBase64))
        {
            Guid naslovnaVersion = Guid.NewGuid();
            var resizedSlika = await _imageManipulationService.ConvertToNaslovnaSlika(updateRadionica.NaslovnaSlikaBase64);
            await _bucketService.UploadImageAsync(naslovnaVersion.ToString(), resizedSlika);
            radionica.NaslovnaSlikaVersion = naslovnaVersion.ToString();
        }

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
        RadionicaDto? objava = _mapper.Map<RadionicaDto?>(await _radioniceRepository.GetRadionicaById(id));

        bool isModerator = await _authorizationService.HasPermission(LevelPristupaEnum.Moderator);

        if (objava == null || !(_ownershipService.Owns(objava) || isModerator)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

        await _radioniceRepository.DeleteRadionica(id);
        return ServiceResult.Success();
    }

    public async Task<ServiceResult<RadionicaKomentarDto>> CreateKomentar(RadionicaKomentarDto komentar, int radionicaId)
    {
        Korisnik korisnik = _authorizationService.GetKorisnik();

        komentar.VlasnikId = korisnik.Id;
        komentar.CreatedDateTime = DateTime.UtcNow;
        komentar.RadionicaId = radionicaId;

        RadionicaKomentarDto createdKomentar = _mapper.Map<RadionicaKomentarDto>(await _radioniceRepository.CreateKomentar(_mapper.Map<RadionicaKomentar>(komentar)));
        return ServiceResult.Success(createdKomentar);
    }

    public async Task<ServiceResult> DeleteKomentar(int id)
    {
        RadionicaKomentar? komentar = await _radioniceRepository.GetKomentarById(id);

        bool isModerator = await _authorizationService.HasPermission(LevelPristupaEnum.Moderator);

        if (komentar == null || !(_ownershipService.Owns(komentar) || isModerator)) return ServiceResult.Failure(MessageDescriber.Unauthorized());

        if (komentar.IsDeleted) return ServiceResult.Failure(MessageDescriber.BadRequest("Komentar je već izbrisan."));

        var hasPodkomentari = await _radioniceRepository.HasPodkomentari(id);

        await _radioniceRepository.DeleteKomentar(id, keepEntry: hasPodkomentari);
        return ServiceResult.Success();
    }

    public async Task<ServiceResult<List<RadionicaKomentarDto>>> GetKomentarListRecursive(int id, int? nadKomentarId = null)
    {
        List<RadionicaKomentarDto> komentari = _mapper.Map<List<RadionicaKomentarDto>>(await _radioniceRepository.GetPodKomentarList(id, nadKomentarId));
        Korisnik? korisnik = _authorizationService.GetKorisnikOptional();

        foreach (var komentar in komentari)
        {
            if (korisnik != null)
            {
                komentar.Liked = (await _radioniceRepository.GetKomentarRadionicaReakcija(komentar.Id, korisnik.Id))?.Liked;
            }
            komentar.PodKomentarList = _mapper.Map<List<RadionicaKomentarDto>>((await GetKomentarListRecursive(id, komentar.Id)).Data);
        }

        return ServiceResult.Success(komentari);
    }

    public async Task<ServiceResult<RadionicaKomentarDto>> UpdateKomentar(UpdateKomentarRadionicaDto updateKomentar)
    {
        var komentar = await _radioniceRepository.GetKomentarRadionicaByIdWithTracking(updateKomentar.Id);

        if (komentar == null || !_ownershipService.Owns(komentar) || komentar.IsDeleted) return ServiceResult.Failure(MessageDescriber.Unauthorized());

        komentar.Sadrzaj = updateKomentar.Sadrzaj;
        komentar.LastUpdatedDateTime = DateTime.UtcNow;

        var updatedKomentar = _mapper.Map<RadionicaKomentarDto>(await _radioniceRepository.UpdateKomentar(komentar));

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

    public async Task<ServiceResult> CreateOcjena(int radionicaId, double ocjena)
    {
        if (ocjena < 1 || ocjena > 5) return ServiceResult.Failure(MessageDescriber.BadRequest("Ocjena mora biti između 1 i 5."));

        var korisnik = _authorizationService.GetKorisnik();

        var postojecaOcjena = await _radioniceRepository.GetRadionicaOcjenaWithTracking(radionicaId, korisnik.Id);

        if (postojecaOcjena != null)
        {
            postojecaOcjena.Ocjena = ocjena;
            await _radioniceRepository.UpdateRadionicaOcjena(postojecaOcjena);
            return ServiceResult.Success();
        }

        var novaOcjena = new RadionicaOcjena
        {
            RadionicaId = radionicaId,
            KorisnikId = korisnik.Id,
            Ocjena = ocjena
        };

        await _radioniceRepository.CreateOcjena(novaOcjena);
        return ServiceResult.Success();
    }

    public async Task<ServiceResult> DeleteOcjena(int radionicaId)
    {
        var korisnik = _authorizationService.GetKorisnik();
        await _radioniceRepository.DeleteOcjena(radionicaId, korisnik.Id);
        return ServiceResult.Success();
    }

    public async Task<ServiceResult> SendObavijest(ObavijestDto obavijest)
    {
        var prijave = await _radioniceRepository.GetPrijaveForRadionica(obavijest.RadionicaId);
        var radionica = await _radioniceRepository.GetRadionicaByIdWithTracking(obavijest.RadionicaId);

        if (radionica == null || radionica.ObavijestPoslana == true) return ServiceResult.Failure(MessageDescriber.Unauthorized());

        if (radionica.VlasnikId != _authorizationService.GetKorisnik().Id)
        {
            return ServiceResult.Failure(MessageDescriber.Unauthorized());
        }

        if (prijave.Count == 0)
        {
            return ServiceResult.Failure(MessageDescriber.BadRequest("Nitko nije prijavljen na radionicu... :("));
        }

        radionica.ObavijestPoslana = true;
        await _radioniceRepository.UpdateRadionica(radionica);

        string[] mails = [];

        foreach (var prijava in prijave)
        {
            if (!string.IsNullOrEmpty(prijava.Korisnik.Email))
            {
                mails = mails.Append(prijava.Korisnik.Email).ToArray();
            }
        }

        MailMessage message = new()
        {
            Subject = WebUtility.HtmlEncode(obavijest.Naslov),
            Body = MailDescriber.RadionicaPrijava(WebUtility.HtmlEncode(radionica.Naziv), WebUtility.HtmlEncode(obavijest.Poruka)),
            IsBodyHtml = true
        };

        await _mailSenderService.SendMail(message, mails);

        return ServiceResult.Success();
    }


    public async Task<ServiceResult<List<RadionicaDto>>> GetAllRadioniceFavoritForCurrentUser()
    {
        var korisnik = _authorizationService.GetKorisnik();
        var radionice = await _radioniceRepository.GetAllRadioniceFavoritForCurrentUser(korisnik.Id);
        var radioniceDto = _mapper.Map<List<RadionicaDto>>(radionice);
        return ServiceResult.Success(radioniceDto);
    }

    public async Task<ServiceResult> AddFavoritRadionica(int radionicaId)
    {
        var korisnik = _authorizationService.GetKorisnik();

        var vecFavorit = await _radioniceRepository.VecFavorit(radionicaId, korisnik.Id);

        if (vecFavorit) return ServiceResult.Failure(MessageDescriber.BadRequest("Već u favoritima."));

        RadionicaFavorit favorit = new()
        {
            KorisnikId = korisnik.Id,
            RadionicaId = radionicaId
        };
        await _radioniceRepository.AddFavoritRadionica(favorit);
        return ServiceResult.Success();
    }

    public async Task<ServiceResult> RemoveFavoritRadionica(int favoritRadionicaId)
    {
        var korisnik = _authorizationService.GetKorisnik();
        await _radioniceRepository.RemoveFavoritRadionica(favoritRadionicaId, korisnik.Id);
        return ServiceResult.Success();
    }

    public async Task<ServiceResult<List<KorisnikDto>>> GetPrijaveZaRadionicu(int radionicaId)
    {
        var korisnik = _authorizationService.GetKorisnik();

        var radionica = await _radioniceRepository.GetRadionica(radionicaId);

        if (radionica == null || korisnik.Id != radionica.VlasnikId) return ServiceResult.Failure(MessageDescriber.Unauthorized());

        return ServiceResult.Success(_mapper.Map<List<KorisnikDto>>(await _radioniceRepository.GetKorisniciPrijavitiZaRadionicu(radionicaId)));
    }
}