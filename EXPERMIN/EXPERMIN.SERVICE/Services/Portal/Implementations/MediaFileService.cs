using AutoMapper;
using EXPERMIN.CORE.Helpers;
using EXPERMIN.CORE.Services.Implementations;
using EXPERMIN.CORE.Services.Interfaces;
using EXPERMIN.ENTITIES.Models;
using EXPERMIN.REPOSITORY.Repositories.Portal.Interfaces;
using EXPERMIN.REPOSITORY.Repositories.Storage.Implementations;
using EXPERMIN.REPOSITORY.Repositories.Storage.Interfaces;
using EXPERMIN.SERIVICE.Services.Interfaces;
using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.Portal.Banner;
using EXPERMIN.SERVICE.Dtos.Portal.MediFile;
using EXPERMIN.SERVICE.Services.Portal.Interfaces;
using EXPERMIN.SERVICE.Services.User.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EXPERMIN.CORE.Helpers.ConstantHelpers;

namespace EXPERMIN.SERVICE.Services.Portal.Implementations
{
    public class MediaFileService: IMediaFileService
    {
        private readonly IMediaFileRepository _mediaFileRepository;
        private readonly IUserValidationService _userValidationService;
        private readonly IMapper _mapper;
        private readonly IFileValidatorServices _fileValidatorServices;
        private readonly IFileStorageService _fileStorageService;
        public MediaFileService(IMediaFileRepository mediaFileRepository, IUserValidationService userValidationService, 
            IMapper mapper, IFileValidatorServices fileValidatorServices, IFileStorageService fileStorageService)
        {
            _mediaFileRepository = mediaFileRepository;
            _userValidationService = userValidationService;
            _mapper = mapper;
            _fileValidatorServices = fileValidatorServices;
            _fileStorageService = fileStorageService;
        }
        public async Task<OperationDto<MediaFileDto>> Upload(string userLoggedId, UploadMediaFileDto dto)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<MediaFileDto>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            if (dto.File == null || dto.File.Length == 0)
                return new OperationDto<MediaFileDto>(OperationCodeDto.InvalidParameters, "El archivo es obligatorio");

            // 1. Validar Imagen
            var imgValidated = _fileValidatorServices.ValidateImage(dto.File);
            if (!imgValidated.Success)
                return new OperationDto<MediaFileDto>(OperationCodeDto.InvalidInput, imgValidated.Message);

            // 2. Guardar archivo en disco/S3/etc.
            var storageResult = await _fileStorageService.SaveFileAsync(dto.File);

            // 3. Registrar en BD
            var mediaFile = new MediaFile
            {
                Id = Guid.NewGuid(),
                FileName = dto.File.FileName,
                Path = storageResult.Path,
                Url = storageResult.Url,
                ContentType = dto.File.ContentType,
                Size = dto.File.Length,
                UploadDate = DateTime.UtcNow,
                IsTemporary = true
            };

            await _mediaFileRepository.Insert(mediaFile);

            // Mapear resto de propiedades
            var mediaFileDto = _mapper.Map<MediaFileDto>(mediaFile);
            return new OperationDto<MediaFileDto>(mediaFileDto);
        }
    }
}
