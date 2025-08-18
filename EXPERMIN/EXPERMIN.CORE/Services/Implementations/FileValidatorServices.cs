using EXPERMIN.CORE.Services.Interfaces;
using EXPERMIN.CORE.Services.Models.Generic;
using EXPERMIN.CORE.Services.Models.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.CORE.Services.Implementations
{
    public class FileValidatorServices : IFileValidatorServices
    {
        public FileValidatorServices()
        {

        }
        public ResponseCore ValidateImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return new ResponseCore()
                {
                    Success = false,
                    Message = "El archivo no puede estar vacío."
                };

            // Validar tamaño
            if (file.Length < ImageValidationConstants.MinFileSize)
                return new ResponseCore()
                {
                    Success = false,
                    Message = $"El archivo es demasiado pequeño(mínimo {ImageValidationConstants.MinFileSize / 1024} KB)."
                };
            if (file.Length > ImageValidationConstants.MaxFileSize)
                return new ResponseCore()
                {
                    Success = false,
                    Message = $"El archivo excede el tamaño máximo permitido ({ImageValidationConstants.MaxFileSize / 1024 / 1024} MB)."
                };
            // Validar extensión
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!ImageValidationConstants.AllowedExtensions.Contains(extension))
                return new ResponseCore() { Success = false, Message = $"Extensión no permitida: {extension}" };

            // Validar MIME type
            if (!ImageValidationConstants.AllowedMimeTypes.Contains(file.ContentType))
                return new ResponseCore() { Success = false, Message = $"Tipo de contenido no permitido: {file.ContentType}" };

            return new ResponseCore() { Success = true, Message = file.FileName };
        }
    }
}
