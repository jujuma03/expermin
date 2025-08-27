using AutoMapper;
using EXPERMIN.CORE.Extensions;
using EXPERMIN.CORE.Helpers;
using EXPERMIN.ENTITIES.Models;
using EXPERMIN.REPOSITORY.Repositories.Portal.Interfaces;
using EXPERMIN.REPOSITORY.Repositories.Storage.Interfaces;
using EXPERMIN.SERIVICE.Services.Interfaces;
using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.Portal.MediFile;
using EXPERMIN.SERVICE.Dtos.Portal.Testimony;
using EXPERMIN.SERVICE.Services.Portal.Interfaces;
using EXPERMIN.SERVICE.Services.User.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Services.Portal.Implementations
{

    public class TestimonyService : ITestimonyService
    {
        private readonly ITestimonyRepository _testimonyRepository;
        private readonly IUserValidationService _userValidationService;
        private readonly IMapper _mapper;
        private readonly IMediaFileRepository _mediaFileRepository;
        private readonly IFileStorageService _fileStorageService;
        public TestimonyService(ITestimonyRepository testimonyRepository, IUserValidationService userValidationService, IMapper mapper,
            IMediaFileRepository mediaFileRepository, IFileStorageService fileStorageService)
        {
            _testimonyRepository = testimonyRepository;
            _userValidationService = userValidationService;
            _mapper = mapper;
            _mediaFileRepository = mediaFileRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<OperationDto<List<TestimonyDto>>> GetAllTestimoniesActive()
        {
            var testimonies = await _testimonyRepository
                .GetAsQueryable()
                .AsNoTracking()
                .Where(x => x.Status == ConstantHelpers.TESTIMONY.STATUS.ACTIVE)
                .Select(x => new TestimonyDto
                {
                    Id = x.Id.ToString(),
                    ClientName = x.ClientName,
                    PublicationDate = x.PublicationDate.ToLocalDateTimeFormat(),
                    StatusId = x.Status,
                    Status = ConstantHelpers.TESTIMONY.STATUS.VALUES[x.Status],
                    OrderId = x.Order ?? 0,
                    Order = (x.Order.HasValue && x.Order.Value != 0)
                            ? ConstantHelpers.ORDER.VALUES[x.Order.Value]
                            : ConstantHelpers.ORDER.VALUES[ConstantHelpers.ORDER.NO_ORDER],
                    Comment = x.Comment,
                    Rating = x.Rating,
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

            if (!testimonies.Any())
                return new OperationDto<List<TestimonyDto>>(OperationCodeDto.EmptyResult, "No hay testimonios activos.");

            return new OperationDto<List<TestimonyDto>>(testimonies);
        }
        public async Task<OperationDto<List<TestimonyDto>>> GetAllTestimonies(string userLoggedId)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<List<TestimonyDto>>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            var testimonys = await _testimonyRepository
                .GetAsQueryable()
                .Select(x => new TestimonyDto
                {
                    Id = x.Id.ToString(),
                    ClientName = x.ClientName,
                    PublicationDate = x.PublicationDate.ToLocalDateTimeFormat(),
                    StatusId = x.Status,
                    Status = ConstantHelpers.TESTIMONY.STATUS.VALUES[x.Status],
                    OrderId = x.Order ?? 0,
                    Order = (x.Order.HasValue && x.Order.Value != 0)
                            ? ConstantHelpers.ORDER.VALUES[x.Order.Value]
                            : ConstantHelpers.ORDER.VALUES[ConstantHelpers.ORDER.NO_ORDER],
                    Comment = x.Comment,
                    Rating = x.Rating,
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

            if (!testimonys.Any())
                return new OperationDto<List<TestimonyDto>>(OperationCodeDto.EmptyResult, "No hay testimonios registrados.");

            return new OperationDto<List<TestimonyDto>>(testimonys);
        }
        public async Task<OperationDto<TestimonyDto>> GetTestimony(string userLoggedId, Guid id)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<TestimonyDto>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            if (id == Guid.Empty)
                return new OperationDto<TestimonyDto>(OperationCodeDto.InvalidParameters, "El ID es inválido.");

            var testimony = await _testimonyRepository.GetTestimonyDetail(id);

            if (testimony == null)
                return new OperationDto<TestimonyDto>(OperationCodeDto.DoesNotExist, "El testimonio no existe.");

            var dto = new TestimonyDto
            {
                Id = testimony.Id.ToString(),
                ClientName = testimony.ClientName,
                PublicationDate = testimony.PublicationDate.ToLocalDateTimeFormat(),
                StatusId = testimony.Status,
                Status = ConstantHelpers.TESTIMONY.STATUS.VALUES[testimony.Status],
                OrderId = testimony.Order ?? 0,
                Order = (testimony.Order.HasValue && testimony.Order.Value != 0)
                            ? ConstantHelpers.ORDER.VALUES[testimony.Order.Value]
                            : ConstantHelpers.ORDER.VALUES[ConstantHelpers.ORDER.NO_ORDER],
                Comment = testimony.Comment,
                Rating = testimony.Rating,
                Image = testimony.MediaFile == null ? null : new MediaFileDto
                {
                    Id = testimony.MediaFile.Id,
                    FileName = testimony.MediaFile.FileName,
                    Path = testimony.MediaFile.Path,
                    Url = testimony.MediaFile.Url,
                    ContentType = testimony.MediaFile.ContentType,
                    Size = testimony.MediaFile.Size,
                    UploadDate = testimony.MediaFile.UploadDate.ToLocalDateTimeFormat(),
                    IsTemporary = testimony.MediaFile.IsTemporary
                }
            };

            return new OperationDto<TestimonyDto>(dto);
        }
        public async Task<OperationDto<TestimonyDto>> GetTestimonyActive(Guid id)
        {
            if (id == Guid.Empty)
                return new OperationDto<TestimonyDto>(OperationCodeDto.InvalidParameters, "El ID es inválido.");

            var testimony = await _testimonyRepository.GetTestimonyDetail(id);

            if (testimony == null || testimony.Status == ConstantHelpers.TESTIMONY.STATUS.HIDDEN)
                return new OperationDto<TestimonyDto>(OperationCodeDto.DoesNotExist, "El testimonio no existe.");

            var dto = new TestimonyDto
            {
                Id = testimony.Id.ToString(),
                ClientName = testimony.ClientName,
                PublicationDate = testimony.PublicationDate.ToLocalDateTimeFormat(),
                StatusId = testimony.Status,
                Status = ConstantHelpers.TESTIMONY.STATUS.VALUES[testimony.Status],
                OrderId = testimony.Order ?? 0,
                Order = (testimony.Order.HasValue && testimony.Order.Value != 0)
                            ? ConstantHelpers.ORDER.VALUES[testimony.Order.Value]
                            : ConstantHelpers.ORDER.VALUES[ConstantHelpers.ORDER.NO_ORDER],
                Comment = testimony.Comment,
                Rating = testimony.Rating,
                Image = testimony.MediaFile == null ? null : new MediaFileDto
                {
                    Id = testimony.MediaFile.Id,
                    FileName = testimony.MediaFile.FileName,
                    Path = testimony.MediaFile.Path,
                    Url = testimony.MediaFile.Url,
                    ContentType = testimony.MediaFile.ContentType,
                    Size = testimony.MediaFile.Size,
                    UploadDate = testimony.MediaFile.UploadDate.ToLocalDateTimeFormat(),
                    IsTemporary = testimony.MediaFile.IsTemporary
                }
            };

            return new OperationDto<TestimonyDto>(dto);
        }
        public async Task<OperationDto<ResponseDto>> InsertTestimony(string userLoggedId, TestimonyRegisterDto model)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<ResponseDto>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            if (model == null)
                return new OperationDto<ResponseDto>(OperationCodeDto.InvalidParameters, "El testimonio no puede ser nulo.");

            // Validación de imágenes obligatorias (ejemplo: al menos 1 archivo)
            if (model.ImageId == Guid.Empty)
                return new OperationDto<ResponseDto>(OperationCodeDto.InvalidParameters, "Debe adjuntar una imagen.");

            // Obtener imagen
            var image = await _mediaFileRepository.Get(model.ImageId);
            if (image == null)
                return new OperationDto<ResponseDto>(OperationCodeDto.DoesNotExist, "No existe la imagen adjuntada al testimonio.");

            // Validar que pueda insertar el testimonyo en la orden indicada
            var testimonyActive = await GetAllTestimoniesActive();
            if (testimonyActive?.Result?.Any(b => b.OrderId == model.Order) == true)
                return new OperationDto<ResponseDto>( OperationCodeDto.OperationError, "Ya hay otro testimonio en esa posición/orden de visualización.");

            // si lo inserta como oculto, no debería tener orden
            if (model.Status == ConstantHelpers.TESTIMONY.STATUS.HIDDEN || model.Status == 0)
                model.Order = ConstantHelpers.ORDER.NO_ORDER;
            // si lo inserta sin orden, debería ser oculto
            if (model.Order == ConstantHelpers.ORDER.NO_ORDER || model.Order == 0)
                model.Status = ConstantHelpers.TESTIMONY.STATUS.HIDDEN;

            var testimony = new Testimony()
            {
                ClientName = model.ClientName,
                Comment = model.Comment,
                Rating = (byte)model.Rating,
                Status = (byte)model.Status,
                PublicationDate = DateTime.UtcNow,
                Order = (byte)model.Order,
                // Mapeo de archivos asociados al testimony
                MediaFileId = image.Id
            };

            // --- Transacción ---
            return await _testimonyRepository.ExecuteInTransactionWithSaveAsync(async () =>
            {
                await _testimonyRepository.Add(testimony);

                var fileName = Path.GetFileName(image.Path); // mismo nombre del archivo
                // Mover de Temporales → banners
                var (newPath, newUrl) = await _fileStorageService.MoveFileAsync(image.Path, "testimonies", fileName);

                // Actualizar imagen
                image.Path = newPath;
                image.Url = newUrl;
                image.IsTemporary = false;
                _mediaFileRepository.Attach(image);

                return new OperationDto<ResponseDto>(
                    new ResponseDto { Suceso = true, Mensaje = "Se ha insertado el testimonio correctamente." });
            });
        }
        public async Task<OperationDto<ResponseDto>> UpdateTestimony(string userLoggedId, Guid testimonyId, TestimonyUpdateDto model)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<ResponseDto>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            if (model == null)
                return new OperationDto<ResponseDto>(OperationCodeDto.InvalidParameters, "El testimonio no puede ser nulo.");

            // Buscar testimony
            var testimony = await _testimonyRepository.GetTestimonyDetail(testimonyId);
            if (testimony == null)
                return new OperationDto<ResponseDto>(OperationCodeDto.DoesNotExist, "El testimonio no existe.");

            // Validar que no se repita el orden
            var testimonyActive = await GetAllTestimoniesActive();
            if (testimonyActive?.Result?.Any(b => b.OrderId == model.Order) == true)
                return new OperationDto<ResponseDto>(OperationCodeDto.OperationError, "Ya hay otro testimonio en esa posición/orden de visualización.");

            // si lo actualiza como oculto, no debería tener orden
            if (model.Status == ConstantHelpers.TESTIMONY.STATUS.HIDDEN)
                model.Order = ConstantHelpers.ORDER.NO_ORDER;
            // si lo actualiza sin orden, debería ser oculto
            if (model.Order == ConstantHelpers.ORDER.NO_ORDER)
                model.Status = ConstantHelpers.TESTIMONY.STATUS.HIDDEN;

            Guid? oldImageId = null;

            // Mapear resto de propiedades (menos la imagen, que manejamos manualmente)
            _mapper.Map(model, testimony);

            return await _testimonyRepository.ExecuteInTransactionWithSaveAsync(async () =>
            {
                // Si viene imagen nueva diferente a la actual → swap
                if (model.ImageId.HasValue && model.ImageId.Value != testimony.MediaFileId)
                {
                    // Verificar que el nuevo MediaFile exista
                    var newImage = await _mediaFileRepository.Get(model.ImageId.Value);
                    if (newImage == null)
                        return new OperationDto<ResponseDto>(OperationCodeDto.DoesNotExist, "La nueva imagen no existe en el sistema.");

                    // mover archivo desde Temporales → testimony
                    var fileName = Path.GetFileName(newImage.Path);
                    var (newPath, newUrl) = await _fileStorageService.MoveFileAsync(newImage.Path, "testimonies", fileName);

                    newImage.Path = newPath;
                    newImage.Url = newUrl;
                    newImage.IsTemporary = false;

                    // actualizar testimony con la nueva imagen
                    oldImageId = testimony.MediaFileId;
                    testimony.MediaFileId = newImage.Id;

                    // actulizar imagen
                    _mediaFileRepository.Update(newImage);
                }

                // Guardar cambios del testimony
                _testimonyRepository.Update(testimony);

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
                return new OperationDto<ResponseDto>(new ResponseDto() { Suceso = true, Mensaje = "Testimonio actualizado Correctamente" });
            });
        }
        public async Task<OperationDto<ResponseDto>> DeleteTestimony(string userLoggedId, Guid testimonyId)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<ResponseDto>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            // Buscar testimony
            var testimony = await _testimonyRepository.GetTestimonyDetail(testimonyId);
            if (testimony == null)
                return new OperationDto<ResponseDto>(OperationCodeDto.DoesNotExist, "El testimonio no existe.");

            // Transacción
            return await _testimonyRepository.ExecuteInTransactionWithSaveAsync(async () =>
            {
                // Eliminar testimony
                _testimonyRepository.Delete(testimony);

                // Eliminar mediaFile asociado
                var mediaFile = await _mediaFileRepository.Get(testimony.MediaFileId);
                if (mediaFile != null)
                {
                    await _fileStorageService.DeleteFileAsync(mediaFile.Path);
                    _mediaFileRepository.Delete(mediaFile);
                }

                return new OperationDto<ResponseDto>(
                    new ResponseDto()
                    {
                        Suceso = true,
                        Mensaje = "Testimonio eliminado correctamente"
                    });
            });
        }
    }
}
