using AutoMapper;
using EXPERMIN.CORE.Extensions;
using EXPERMIN.CORE.Helpers;
using EXPERMIN.ENTITIES.Models;
using EXPERMIN.REPOSITORY.Repositories.Portal.Interfaces;
using EXPERMIN.REPOSITORY.Repositories.Storage.Interfaces;
using EXPERMIN.SERIVICE.Services.Interfaces;
using EXPERMIN.SERIVICE.Structs;
using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.Portal.Banner;
using EXPERMIN.SERVICE.Dtos.Portal.MediFile;
using EXPERMIN.SERVICE.Dtos.User;
using EXPERMIN.SERVICE.Services.Portal.Interfaces;
using EXPERMIN.SERVICE.Services.User.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EXPERMIN.CORE.Helpers.ConstantHelpers;
using static EXPERMIN.CORE.Helpers.ConstantHelpers.BANNER;

namespace EXPERMIN.SERVICE.Services.Portal.Implementations
{
    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _bannerRepository;
        private readonly IUserValidationService _userValidationService;
        private readonly IMapper _mapper;
        private readonly IMediaFileRepository _mediaFileRepository;
        private readonly IFileStorageService _fileStorageService;
        public BannerService(IBannerRepository bannerRepository, IUserValidationService userValidationService, IMapper mapper, 
            IMediaFileRepository mediaFileRepository, IFileStorageService fileStorageService)
        {
            _bannerRepository = bannerRepository;
            _userValidationService = userValidationService;
            _mapper = mapper;
            _mediaFileRepository = mediaFileRepository;
            _fileStorageService = fileStorageService;
        }
        public async Task<OperationDto<DataTablesStructs.ReturnedData<BannerDto>>> GetAllBannerDatatable(DataTablesStructs.SentDataTableParameters sentParameters, string headline = null, byte? status = null)
        {
            var totalRecords = await _bannerRepository.GetAsQueryable().CountAsync();

            //validar si existe el banner
            if (totalRecords == 0)
                return new OperationDto<DataTablesStructs.ReturnedData<BannerDto>>(OperationCodeDto.EmptyResult, "No existen Banners registrados.");

            var query = _bannerRepository.GetAsQueryable();

            if (!string.IsNullOrEmpty(headline))
                query = query.Where(q => q.Headline.Contains(headline));

            if (status.HasValue && status.Value != 0)
                query = query.Where(q => q.Status == status.Value);

            var recordsFiltered = await query.CountAsync();

            var data = await query
                .Skip(sentParameters.PagingFirstRecord)
                .Take(sentParameters.RecordsPerDraw)
                .Select(x => new BannerDto
                {
                    Id = x.Id.ToString(),
                    Headline = x.Headline,
                    PublicationDate = x.PublicationDate.ToLocalDateTimeFormat(),
                    StatusId = x.Status,
                    Status = ConstantHelpers.BANNER.STATUS.VALUES[x.Status],
                    Order = (x.Order.HasValue && x.Order.Value != 0)
                            ? ConstantHelpers.ORDER.VALUES[x.Order.Value]
                            : ConstantHelpers.ORDER.VALUES[ConstantHelpers.ORDER.NO_ORDER],
                    OrderId = x.Order ?? 0,
                    Image = x.MediaFile == null ? null : new MediaFileDto
                    {
                        Id = x.MediaFile.Id,
                        FileName = x.MediaFile.FileName,
                        Path = x.MediaFile.Path,
                        Url = x.MediaFile.Url,
                        ContentType = x.MediaFile.ContentType,
                        Size = x.MediaFile.Size,
                        UploadDate = x.MediaFile.UploadDate.ToLocalDateTimeFormat(),
                        IsTemporary = x.MediaFile.IsTemporary
                    }
                }).ToListAsync();

            var result = new DataTablesStructs.ReturnedData<BannerDto>
            {
                Data = data,
                DrawCounter = sentParameters.DrawCounter,
                RecordsFiltered = recordsFiltered,
                TotalRecords = totalRecords
            };

            return new OperationDto<DataTablesStructs.ReturnedData<BannerDto>>(result);
        }
        public async Task<OperationDto<IEnumerable<KeyValuePair<int, string>>>> GetAvailableOrdersAndListSequenceOrder()
        {
            // Obtengo todos los SequenceOrder existentes en BD (solo los que no son null)
            var usedOrders = await _bannerRepository
                .GetAsQueryable()
                .Where(b => b.Order.HasValue)
                .Select(b => (int)b.Order.Value)
                .ToListAsync();

            var availableOrders = ConstantHelpers.ORDER.VALUES
                .Where(x => !usedOrders.Contains(x.Key));

            if (!availableOrders.Any())
                return new OperationDto<IEnumerable<KeyValuePair<int, string>>>(OperationCodeDto.EmptyResult, "No hay banner disponible.");

            return new OperationDto<IEnumerable<KeyValuePair<int, string>>>(availableOrders);
        }
        public async Task<OperationDto<List<BannerDto>>> GetAllBannersActive()
        {
            var banners = await _bannerRepository
                .GetAsQueryable()
                .AsNoTracking()
                .Where(x => x.Status == ConstantHelpers.BANNER.STATUS.ACTIVE)
                .Select(x => new BannerDto
                {
                    Id = x.Id.ToString(),
                    Headline = x.Headline,
                    PublicationDate = x.PublicationDate.ToLocalDateTimeFormat(),
                    StatusId = x.Status,
                    Status = ConstantHelpers.BANNER.STATUS.VALUES[x.Status],
                    Order = (x.Order.HasValue && x.Order.Value != 0)
                            ? ConstantHelpers.ORDER.VALUES[x.Order.Value]
                            : ConstantHelpers.ORDER.VALUES[ConstantHelpers.ORDER.NO_ORDER],
                    OrderId = x.Order ?? 0,
                    Description = x.Description,
                    NameDirection = x.NameDirection,
                    RouteType = x.RouteType,
                    StatusDirection = x.StatusDirection,
                    UrlDirection = x.UrlDirection,
                    Image = x.MediaFile == null ? null : new MediaFileDto
                    {
                        Id = x.MediaFile.Id,
                        FileName = x.MediaFile.FileName,
                        Path = x.MediaFile.Path,
                        Url = x.MediaFile.Url,
                        ContentType = x.MediaFile.ContentType,
                        Size = x.MediaFile.Size,
                        UploadDate = x.MediaFile.UploadDate.ToLocalDateTimeFormat(),
                        IsTemporary = x.MediaFile.IsTemporary
                    }
                })
                .ToListAsync();

            if (!banners.Any())
                return new OperationDto<List<BannerDto>>(OperationCodeDto.EmptyResult, "No hay banners activos.");

            return new OperationDto<List<BannerDto>>(banners);
        }
        public async Task<OperationDto<List<BannerDto>>> GetAllBanners(string userLoggedId)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<List<BannerDto>>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            var banners = await _bannerRepository
                .GetAsQueryable()
                .Select(x => new BannerDto
                {
                    Id = x.Id.ToString(),
                    Headline = x.Headline,
                    PublicationDate = x.PublicationDate.ToLocalDateTimeFormat(),
                    StatusId = x.Status,
                    Status = ConstantHelpers.BANNER.STATUS.VALUES[x.Status],
                    Order = (x.Order.HasValue && x.Order.Value != 0)
                            ? ConstantHelpers.ORDER.VALUES[x.Order.Value]
                            : ConstantHelpers.ORDER.VALUES[ConstantHelpers.ORDER.NO_ORDER],
                    OrderId = x.Order ?? 0,
                    UrlDirection = x.UrlDirection,
                    StatusDirection = x.StatusDirection,
                    RouteType = x.RouteType,
                    NameDirection = x.NameDirection,
                    Description = x.Description,
                    Image = x.MediaFile == null ? null : new MediaFileDto
                    {
                        Id = x.MediaFile.Id,
                        FileName = x.MediaFile.FileName,
                        Path = x.MediaFile.Path,
                        Url = x.MediaFile.Url,
                        ContentType = x.MediaFile.ContentType,
                        Size = x.MediaFile.Size,
                        UploadDate = x.MediaFile.UploadDate.ToLocalDateTimeFormat(),
                        IsTemporary = x.MediaFile.IsTemporary
                    }
                })
                .ToListAsync();

            if (!banners.Any())
                return new OperationDto<List<BannerDto>>(OperationCodeDto.EmptyResult, "No hay banners registrados.");

            return new OperationDto<List<BannerDto>>(banners);
        }
        public async Task<OperationDto<BannerDto>> GetBanner(string userLoggedId, Guid id)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<BannerDto>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            if (id == Guid.Empty)
                return new OperationDto<BannerDto>(OperationCodeDto.InvalidParameters, "El ID es inválido.");

            var banner = await _bannerRepository.GetBannerDetail(id);

            if (banner == null)
                return new OperationDto<BannerDto>(OperationCodeDto.DoesNotExist, "El banner no existe.");

            var dto = new BannerDto
            {
                Id = banner.Id.ToString(),
                Headline = banner.Headline,
                PublicationDate = banner.PublicationDate.ToLocalDateTimeFormat(),
                StatusId = banner.Status,
                Status = ConstantHelpers.BANNER.STATUS.VALUES[banner.Status],
                Order = (banner.Order.HasValue && banner.Order.Value != 0)
                            ? ConstantHelpers.ORDER.VALUES[banner.Order.Value]
                            : ConstantHelpers.ORDER.VALUES[ConstantHelpers.ORDER.NO_ORDER],
                OrderId = banner.Order ?? 0,
                Description = banner.Description,
                NameDirection = banner.NameDirection,
                RouteType = banner.RouteType,
                StatusDirection = banner.StatusDirection,
                UrlDirection = banner.UrlDirection,
                Image = banner.MediaFile == null ? null : new MediaFileDto
                {
                    Id = banner.MediaFile.Id,
                    FileName = banner.MediaFile.FileName,
                    Path = banner.MediaFile.Path,
                    Url = banner.MediaFile.Url,
                    ContentType = banner.MediaFile.ContentType,
                    Size = banner.MediaFile.Size,
                    UploadDate = banner.MediaFile.UploadDate.ToLocalDateTimeFormat(),
                    IsTemporary = banner.MediaFile.IsTemporary
                }
            };

            return new OperationDto<BannerDto>(dto);
        }
        public async Task<OperationDto<BannerDto>> GetBannerActive(Guid id)
        {
            if (id == Guid.Empty)
                return new OperationDto<BannerDto>(OperationCodeDto.InvalidParameters, "El ID es inválido.");

            var banner = await _bannerRepository.GetBannerDetail(id);

            if (banner == null || banner.Status == ConstantHelpers.BANNER.STATUS.HIDDEN)
                return new OperationDto<BannerDto>(OperationCodeDto.DoesNotExist, "El banner no existe.");

            var dto = new BannerDto
            {
                Id = banner.Id.ToString(),
                Headline = banner.Headline,
                PublicationDate = banner.PublicationDate.ToLocalDateTimeFormat(),
                StatusId = banner.Status,
                Status = ConstantHelpers.BANNER.STATUS.VALUES[banner.Status],
                Order = (banner.Order.HasValue && banner.Order.Value != 0)
                            ? ConstantHelpers.ORDER.VALUES[banner.Order.Value]
                            : ConstantHelpers.ORDER.VALUES[ConstantHelpers.ORDER.NO_ORDER],
                OrderId = banner.Order ?? 0,
                UrlDirection = banner.UrlDirection,
                StatusDirection = banner.StatusDirection,
                RouteType = banner.RouteType,
                NameDirection = banner.NameDirection,
                Description = banner.Description,
                Image = banner.MediaFile == null ? null : new MediaFileDto
                {
                    Id = banner.MediaFile.Id,
                    FileName = banner.MediaFile.FileName,
                    Path = banner.MediaFile.Path,
                    Url = banner.MediaFile.Url,
                    ContentType = banner.MediaFile.ContentType,
                    Size = banner.MediaFile.Size,
                    UploadDate = banner.MediaFile.UploadDate.ToLocalDateTimeFormat(),
                    IsTemporary = banner.MediaFile.IsTemporary
                }
            };

            return new OperationDto<BannerDto>(dto);
        }
        public async Task<OperationDto<ResponseDto>> InsertBanner(string userLoggedId, BannerRegisterDto model)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<ResponseDto>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            if (model == null)
                return new OperationDto<ResponseDto>(OperationCodeDto.InvalidParameters, "El banner no puede ser nulo.");

            // Validación de imágenes obligatorias (ejemplo: al menos 1 archivo)
            if (model.ImageId == Guid.Empty)
                return new OperationDto<ResponseDto>(OperationCodeDto.InvalidParameters, "Debe adjuntar una imagen.");

            // Obtener imagen temporal
            var image = await _mediaFileRepository.Get(model.ImageId);
            if(image == null)
                return new OperationDto<ResponseDto>(OperationCodeDto.DoesNotExist, "No existe la imagen adjuntada al banner.");

            // Validar que pueda insertar el banner en la orden indicada, sino se pondrá como oculto y sin orden
            var bannerActive = await GetAllBannersActive();
            if (bannerActive?.Result?.Any() == true)
            {
                if (bannerActive.Result.Count() >= 5)
                {
                    model.Status = ConstantHelpers.BANNER.STATUS.HIDDEN;
                    model.Order = ConstantHelpers.ORDER.NO_ORDER;
                }
                else
                {
                    // Validar que no se repita el orden
                    if (bannerActive.Result.Any(b => b.OrderId == model.Order))
                        return new OperationDto<ResponseDto>(OperationCodeDto.OperationError, "Ya hay otro banner en esa posición/orden de visualización.");
                }
            }

            // si lo inserta como oculto, no debería tener orden
            if (model.Status == ConstantHelpers.BANNER.STATUS.HIDDEN || model.Status == 0)
                model.Order = ConstantHelpers.ORDER.NO_ORDER;
            // si lo inserta sin orden, debería ser oculto
            if (model.Order == ConstantHelpers.ORDER.NO_ORDER || model.Order == 0)
                model.Status = ConstantHelpers.BANNER.STATUS.HIDDEN;

            var banner = new Banner()
            {
                NameDirection = model.NameDirection,
                Description = model.Description,
                Headline = model.Headline,
                Status = (byte)model.Status,
                RouteType = (byte)model.RouteType,
                Order = (byte)model.Order,
                UrlDirection = model.UrlDirection,
                PublicationDate = DateTime.UtcNow,
                StatusDirection = (byte)model.StatusDirection,
                // Mapeo de archivos asociados al banner
                MediaFileId = image.Id
            };

            // --- Transacción ---
            return await _bannerRepository.ExecuteInTransactionWithSaveAsync(async () =>
            {
                await _bannerRepository.Add(banner);

                var fileName = Path.GetFileName(image.Path); // mismo nombre del archivo
                // Mover de Temporales → banners
                var (newPath, newUrl) = await _fileStorageService.MoveFileAsync(image.Path, "banners", fileName);

                // Actualizar imagen
                image.Path = newPath;
                image.Url = newUrl;
                image.IsTemporary = false;
                _mediaFileRepository.Attach(image);


                return new OperationDto<ResponseDto>(
                    new ResponseDto { Suceso = true, Mensaje = "Se ha insertado el banner correctamente. Nota: Si ya hay más de 5 banners, se habrá creado como oculto y sin orden." });
            });
        }
        public async Task<OperationDto<ResponseDto>> UpdateBanner(string userLoggedId, Guid bannerId, BannerUpdateDto model)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<ResponseDto>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            if (model == null)
                return new OperationDto<ResponseDto>(OperationCodeDto.InvalidParameters, "El banner no puede ser nulo.");

            // Buscar banner
            var banner = await _bannerRepository.GetBannerDetail(bannerId);
            if (banner == null)
                return new OperationDto<ResponseDto>(OperationCodeDto.DoesNotExist, "El banner no existe.");

            // Validar que pueda insertar el banner en la orden indicada
            var bannerActive = await GetAllBannersActive();
            if (bannerActive?.Result?.Any() == true)
            {
                if (bannerActive.Result.Count() >= 5)
                {
                    model.Status = ConstantHelpers.BANNER.STATUS.HIDDEN;
                    model.Order = ConstantHelpers.ORDER.NO_ORDER;
                }
                else
                {
                    if (model.Order > 0 &&
                        bannerActive.Result.Any(b => b.OrderId == model.Order &&
                            !string.Equals(b.Id, banner.Id.ToString(), StringComparison.OrdinalIgnoreCase)))
                        return new OperationDto<ResponseDto>(OperationCodeDto.OperationError, "Ya hay otro banner en esa posición.");
                }
            }

            // si lo actualiza como oculto, no debería tener orden
            if (model.Status == ConstantHelpers.BANNER.STATUS.HIDDEN)
                model.Order = ConstantHelpers.ORDER.NO_ORDER;
            // si lo actualiza sin orden, debería ser oculto
            if (model.Order == ConstantHelpers.ORDER.NO_ORDER)
                model.Status = ConstantHelpers.BANNER.STATUS.HIDDEN;

            Guid? oldImageId = null;

            // Mapear resto de propiedades (menos la imagen, que manejamos manualmente)
            _mapper.Map(model, banner);

            return await _bannerRepository.ExecuteInTransactionWithSaveAsync(async () =>
            {
                // Imagen nueva
                if (model.ImageId.HasValue && model.ImageId.Value != banner.MediaFileId)
                {
                    var newImage = await _mediaFileRepository.Get(model.ImageId.Value);
                    if (newImage == null)
                        return new OperationDto<ResponseDto>(OperationCodeDto.DoesNotExist, "La nueva imagen no existe en el sistema.");

                    // mover archivo desde Temporales → banners
                    var fileName = Path.GetFileName(newImage.Path);
                    var (newPath, newUrl) = await _fileStorageService.MoveFileAsync(newImage.Path, "banners", fileName);

                    newImage.Path = newPath;
                    newImage.Url = newUrl;
                    newImage.IsTemporary = false;

                    // actualizar banner con la nueva imagen
                    oldImageId = banner.MediaFileId;
                    banner.MediaFileId = newImage.Id;

                    // actulizar imagen
                    _mediaFileRepository.Update(newImage);
                }

                // Guardar cambios del banner
                _bannerRepository.Update(banner);

                // Eliminar imagen antigua
                if (oldImageId.HasValue)
                {
                    var oldImage = await _mediaFileRepository.Get(oldImageId.Value);
                    if (oldImage != null)
                    {
                        await _fileStorageService.DeleteFileAsync(oldImage.Path);
                        _mediaFileRepository.Delete(oldImage);
                    }
                }

                return new OperationDto<ResponseDto>(new ResponseDto() { Suceso = true, Mensaje = "Banner actualizado Correctamente" });
            });
        }
        public async Task<OperationDto<ResponseDto>> DeleteBanner(string userLoggedId, Guid bannerId)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<ResponseDto>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            // Buscar banner
            var banner = await _bannerRepository.GetBannerDetail(bannerId);
            if (banner == null)
                return new OperationDto<ResponseDto>(OperationCodeDto.DoesNotExist, "El banner no existe.");

            // Transacción
            return await _bannerRepository.ExecuteInTransactionWithSaveAsync(async () =>
            {
                // Eliminar banner
                _bannerRepository.Delete(banner);

                // Eliminar mediaFile asociado
                var mediaFile = await _mediaFileRepository.Get(banner.MediaFileId);
                if (mediaFile != null)
                {
                    await _fileStorageService.DeleteFileAsync(mediaFile.Path);
                    _mediaFileRepository.Delete(mediaFile);
                }

                return new OperationDto<ResponseDto>(
                    new ResponseDto()
                    {
                        Suceso = true,
                        Mensaje = "Banner eliminado correctamente"
                    });
            });
        }
    }
}
