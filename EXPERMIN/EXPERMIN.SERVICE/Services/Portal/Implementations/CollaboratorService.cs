using AutoMapper;
using EXPERMIN.CORE.Helpers;
using EXPERMIN.REPOSITORY.Repositories.Portal.Interfaces;
using EXPERMIN.REPOSITORY.Repositories.Storage.Interfaces;
using EXPERMIN.SERIVICE.Services.Interfaces;
using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.Portal.MediFile;
using EXPERMIN.SERVICE.Dtos.Portal.Collaborator;
using EXPERMIN.SERVICE.Services.Portal.Interfaces;
using EXPERMIN.SERVICE.Services.User.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EXPERMIN.CORE.Extensions;
using EXPERMIN.ENTITIES.Models;

namespace EXPERMIN.SERVICE.Services.Portal.Implementations
{

    public class CollaboratorService : ICollaboratorService
    {
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly IUserValidationService _userValidationService;
        private readonly IMapper _mapper;
        private readonly IMediaFileRepository _mediaFileRepository;
        private readonly IFileStorageService _fileStorageService;
        public CollaboratorService(ICollaboratorRepository collaboratorRepository, IUserValidationService userValidationService, IMapper mapper,
            IMediaFileRepository mediaFileRepository, IFileStorageService fileStorageService)
        {
            _collaboratorRepository = collaboratorRepository;
            _userValidationService = userValidationService;
            _mapper = mapper;
            _mediaFileRepository = mediaFileRepository;
            _fileStorageService = fileStorageService;
        }

        public async Task<OperationDto<List<CollaboratorDto>>> GetAllCollaboratorsActive()
        {
            var collaborators = await _collaboratorRepository
                .GetAsQueryable()
                .AsNoTracking()
                .Where(x => x.Status == ConstantHelpers.COLLABORATOR.STATUS.ACTIVE)
                .Select(x => new CollaboratorDto
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    PublicationDate = x.PublicationDate.ToLocalDateTimeFormat(),
                    StatusId = x.Status,
                    Status = ConstantHelpers.COLLABORATOR.STATUS.VALUES[x.Status],
                    OrderId = x.Order ?? 0,
                    Order = (x.Order.HasValue && x.Order.Value != 0)
                            ? ConstantHelpers.ORDER.VALUES[x.Order.Value]
                            : ConstantHelpers.ORDER.VALUES[ConstantHelpers.ORDER.NO_ORDER],
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

            if (!collaborators.Any())
                return new OperationDto<List<CollaboratorDto>>(OperationCodeDto.EmptyResult, "No hay colaboradores activos.");

            return new OperationDto<List<CollaboratorDto>>(collaborators);
        }
        public async Task<OperationDto<List<CollaboratorDto>>> GetAllCollaborators(string userLoggedId)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<List<CollaboratorDto>>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            var collaborators = await _collaboratorRepository
                .GetAsQueryable()
                .Select(x => new CollaboratorDto
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    PublicationDate = x.PublicationDate.ToLocalDateTimeFormat(),
                    StatusId = x.Status,
                    Status = ConstantHelpers.COLLABORATOR.STATUS.VALUES[x.Status],
                    OrderId = x.Order ?? 0,
                    Order = (x.Order.HasValue && x.Order.Value != 0)
                            ? ConstantHelpers.ORDER.VALUES[x.Order.Value]
                            : ConstantHelpers.ORDER.VALUES[ConstantHelpers.ORDER.NO_ORDER],
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

            if (!collaborators.Any())
                return new OperationDto<List<CollaboratorDto>>(OperationCodeDto.EmptyResult, "No hay colaboradores registrados.");

            return new OperationDto<List<CollaboratorDto>>(collaborators);
        }
        public async Task<OperationDto<CollaboratorDto>> GetCollaborator(string userLoggedId, Guid id)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<CollaboratorDto>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            if (id == Guid.Empty)
                return new OperationDto<CollaboratorDto>(OperationCodeDto.InvalidParameters, "El ID es inválido.");

            var collaborator = await _collaboratorRepository.GetCollaboratorDetail(id);

            if (collaborator == null)
                return new OperationDto<CollaboratorDto>(OperationCodeDto.DoesNotExist, "El colaborador no existe.");

            var dto = new CollaboratorDto
            {
                Id = collaborator.Id.ToString(),
                Name = collaborator.Name,
                PublicationDate = collaborator.PublicationDate.ToLocalDateTimeFormat(),
                StatusId = collaborator.Status,
                Status = ConstantHelpers.COLLABORATOR.STATUS.VALUES[collaborator.Status],
                OrderId = collaborator.Order ?? 0,
                Order = (collaborator.Order.HasValue && collaborator.Order.Value != 0)
                            ? ConstantHelpers.ORDER.VALUES[collaborator.Order.Value]
                            : ConstantHelpers.ORDER.VALUES[ConstantHelpers.ORDER.NO_ORDER],
                Image = collaborator.MediaFile == null ? null : new MediaFileDto
                {
                    Id = collaborator.MediaFile.Id,
                    FileName = collaborator.MediaFile.FileName,
                    Path = collaborator.MediaFile.Path,
                    Url = collaborator.MediaFile.Url,
                    ContentType = collaborator.MediaFile.ContentType,
                    Size = collaborator.MediaFile.Size,
                    UploadDate = collaborator.MediaFile.UploadDate.ToLocalDateTimeFormat(),
                    IsTemporary = collaborator.MediaFile.IsTemporary
                }
            };

            return new OperationDto<CollaboratorDto>(dto);
        }
        public async Task<OperationDto<CollaboratorDto>> GetCollaboratorActive(Guid id)
        {
            if (id == Guid.Empty)
                return new OperationDto<CollaboratorDto>(OperationCodeDto.InvalidParameters, "El ID es inválido.");

            var collaborator = await _collaboratorRepository.GetCollaboratorDetail(id);

            if (collaborator == null || collaborator.Status == ConstantHelpers.COLLABORATOR.STATUS.HIDDEN)
                return new OperationDto<CollaboratorDto>(OperationCodeDto.DoesNotExist, "El colaborador no existe.");

            var dto = new CollaboratorDto
            {
                Id = collaborator.Id.ToString(),
                Name = collaborator.Name,
                PublicationDate = collaborator.PublicationDate.ToLocalDateTimeFormat(),
                StatusId = collaborator.Status,
                Status = ConstantHelpers.COLLABORATOR.STATUS.VALUES[collaborator.Status],
                OrderId = collaborator.Order ?? 0,
                Order = (collaborator.Order.HasValue && collaborator.Order.Value != 0)
                            ? ConstantHelpers.ORDER.VALUES[collaborator.Order.Value]
                            : ConstantHelpers.ORDER.VALUES[ConstantHelpers.ORDER.NO_ORDER],
                Image = collaborator.MediaFile == null ? null : new MediaFileDto
                {
                    Id = collaborator.MediaFile.Id,
                    FileName = collaborator.MediaFile.FileName,
                    Path = collaborator.MediaFile.Path,
                    Url = collaborator.MediaFile.Url,
                    ContentType = collaborator.MediaFile.ContentType,
                    Size = collaborator.MediaFile.Size,
                    UploadDate = collaborator.MediaFile.UploadDate.ToLocalDateTimeFormat(),
                    IsTemporary = collaborator.MediaFile.IsTemporary
                }
            };

            return new OperationDto<CollaboratorDto>(dto);
        }
        public async Task<OperationDto<ResponseDto>> InsertCollaborator(string userLoggedId, CollaboratorRegisterDto model)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<ResponseDto>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            if (model == null)
                return new OperationDto<ResponseDto>(OperationCodeDto.InvalidParameters, "El colaborador no puede ser nulo.");

            // Validación de imágenes obligatorias (ejemplo: al menos 1 archivo)
            if (model.ImageId == Guid.Empty)
                return new OperationDto<ResponseDto>(OperationCodeDto.InvalidParameters, "Debe adjuntar una imagen.");

            // Obtener imagen
            var image = await _mediaFileRepository.Get(model.ImageId);
            if (image == null)
                return new OperationDto<ResponseDto>(OperationCodeDto.DoesNotExist, "No existe la imagen adjuntada al colaborador.");

            // Validar que pueda insertar el collaboratoro en la orden indicada
            var collaboratorActive = await GetAllCollaboratorsActive();
            if (collaboratorActive?.Result?.Any(b => b.OrderId == model.Order) == true)
                return new OperationDto<ResponseDto>(OperationCodeDto.OperationError, "Ya hay otro colaborador en esa posición/orden de visualización.");

            // si lo inserta como oculto, no debería tener orden
            if (model.Status == ConstantHelpers.COLLABORATOR.STATUS.HIDDEN || model.Status == 0)
                model.Order = ConstantHelpers.ORDER.NO_ORDER;
            // si lo inserta sin orden, debería ser oculto
            if (model.Order == ConstantHelpers.ORDER.NO_ORDER || model.Order == 0)
                model.Status = ConstantHelpers.COLLABORATOR.STATUS.HIDDEN;

            var collaborator = new Collaborator()
            {
                Name = model.Name,
                Status = (byte)model.Status,
                PublicationDate = DateTime.UtcNow,
                Order = (byte)model.Order,
                // Mapeo de archivos asociados al collaborator
                MediaFileId = image.Id
            };

            // --- Transacción ---
            return await _collaboratorRepository.ExecuteInTransactionWithSaveAsync(async () =>
            {
                await _collaboratorRepository.Add(collaborator);

                var fileName = Path.GetFileName(image.Path); // mismo nombre del archivo
                // Mover de Temporales → banners
                var (newPath, newUrl) = await _fileStorageService.MoveFileAsync(image.Path, "collaborators", fileName);

                // Actualizar imagen
                image.Path = newPath;
                image.Url = newUrl;
                image.IsTemporary = false;
                _mediaFileRepository.Attach(image);

                return new OperationDto<ResponseDto>(
                    new ResponseDto { Suceso = true, Mensaje = "Se ha insertado el colaborador correctamente." });
            });
        }
        public async Task<OperationDto<ResponseDto>> UpdateCollaborator(string userLoggedId, Guid collaboratorId, CollaboratorUpdateDto model)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<ResponseDto>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            if (model == null)
                return new OperationDto<ResponseDto>(OperationCodeDto.InvalidParameters, "El colaborador no puede ser nulo.");

            // Buscar collaborator
            var collaborator = await _collaboratorRepository.GetCollaboratorDetail(collaboratorId);
            if (collaborator == null)
                return new OperationDto<ResponseDto>(OperationCodeDto.DoesNotExist, "El colaborador no existe.");

            // Validar que no se repita el orden
            var collaboratorActive = await GetAllCollaboratorsActive();
            if (collaboratorActive?.Result?.Any(b => b.OrderId == model.Order) == true)
                return new OperationDto<ResponseDto>(OperationCodeDto.OperationError, "Ya hay otro colaborador en esa posición/orden de visualización.");

            // si lo actualiza como oculto, no debería tener orden
            if (model.Status == ConstantHelpers.COLLABORATOR.STATUS.HIDDEN)
                model.Order = ConstantHelpers.ORDER.NO_ORDER;
            // si lo actualiza sin orden, debería ser oculto
            if (model.Order == ConstantHelpers.ORDER.NO_ORDER)
                model.Status = ConstantHelpers.COLLABORATOR.STATUS.HIDDEN;

            Guid? oldImageId = null;

            // Mapear resto de propiedades (menos la imagen, que manejamos manualmente)
            _mapper.Map(model, collaborator);

            return await _collaboratorRepository.ExecuteInTransactionWithSaveAsync(async () =>
            {
                // Si viene imagen nueva diferente a la actual → swap
                if (model.ImageId.HasValue && model.ImageId.Value != collaborator.MediaFileId)
                {
                    // Verificar que el nuevo MediaFile exista
                    var newImage = await _mediaFileRepository.Get(model.ImageId.Value);
                    if (newImage == null)
                        return new OperationDto<ResponseDto>(OperationCodeDto.DoesNotExist, "La nueva imagen no existe en el sistema.");

                    // mover archivo desde Temporales → collaborator
                    var fileName = Path.GetFileName(newImage.Path);
                    var (newPath, newUrl) = await _fileStorageService.MoveFileAsync(newImage.Path, "collaborators", fileName);

                    newImage.Path = newPath;
                    newImage.Url = newUrl;
                    newImage.IsTemporary = false;

                    // actualizar collaborator con la nueva imagen
                    oldImageId = collaborator.MediaFileId;
                    collaborator.MediaFileId = newImage.Id;

                    // actulizar imagen
                    _mediaFileRepository.Update(newImage);
                }

                // Guardar cambios del collaborator
                _collaboratorRepository.Update(collaborator);

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
                return new OperationDto<ResponseDto>(new ResponseDto() { Suceso = true, Mensaje = "Colaborador actualizado Correctamente" });
            });
        }
        public async Task<OperationDto<ResponseDto>> DeleteCollaborator(string userLoggedId, Guid collaboratorId)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<ResponseDto>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            // Buscar collaborator
            var collaborator = await _collaboratorRepository.GetCollaboratorDetail(collaboratorId);
            if (collaborator == null)
                return new OperationDto<ResponseDto>(OperationCodeDto.DoesNotExist, "El colaborador no existe.");

            // Transacción
            return await _collaboratorRepository.ExecuteInTransactionWithSaveAsync(async () =>
            {
                // Eliminar collaborator
                _collaboratorRepository.Delete(collaborator);

                // Eliminar mediaFile asociado
                var mediaFile = await _mediaFileRepository.Get(collaborator.MediaFileId);
                if (mediaFile != null)
                {
                    await _fileStorageService.DeleteFileAsync(mediaFile.Path);
                    _mediaFileRepository.Delete(mediaFile);
                }

                return new OperationDto<ResponseDto>(
                    new ResponseDto()
                    {
                        Suceso = true,
                        Mensaje = "Colaborador eliminado correctamente"
                    });
            });
        }
    }
}
