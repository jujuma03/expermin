using EXPERMIN.CORE.Services.Interfaces;
using EXPERMIN.SERIVICE.Services.Interfaces;
using EXPERMIN.SERVICE.Storage.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERIVICE.Services.Implementations
{
    public class LocalStorageServices : IFileStorageService
    {
        private readonly StorageOptions _settings;
        public LocalStorageServices(IOptions<StorageOptions> settings)
        {
            _settings = settings.Value;
        }

        public async Task<(string Path, string Url)> SaveFileAsync(IFormFile file, string? subFolder = null)
        {
            // Si no se pasa subFolder, usar "Temporales" como carpeta por defecto
            subFolder ??= "Temporales";

            // Carpeta base (C:\...\upload)
            var uploadsRoot = _settings.BasePath;

            // Carpeta de destino inicial (ej: upload\Temporales)
            var targetFolder = Path.Combine(uploadsRoot, subFolder);

            if (!Directory.Exists(targetFolder))
                Directory.CreateDirectory(targetFolder);

            // Nombre único
            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(targetFolder, uniqueFileName);

            // Guardar archivo en disco
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Construir URL pública desde configuración
            var fileUrl = $"/uploads/{subFolder}/{uniqueFileName}";
            //var fileUrl = $"{_settings.BaseUrl}/{subFolder}/{uniqueFileName}";

            return (filePath, fileUrl);
        }

        public Task DeleteFileAsync(string path)
        {
            if (File.Exists(path))
                File.Delete(path);

            return Task.CompletedTask;
        }
        public async Task<(string Path, string Url)> MoveFileAsync(string currentPath, string targetFolder, string fileName)
        {
            if (!File.Exists(currentPath))
                throw new FileNotFoundException("El archivo a mover no existe.", currentPath);

            // Carpeta destino absoluta (ej: BasePath/banners)
            var destinationFolder = Path.Combine(_settings.BasePath, targetFolder);
            if (!Directory.Exists(destinationFolder))
                Directory.CreateDirectory(destinationFolder);

            // Nueva ruta destino
            var newPath = Path.Combine(destinationFolder, fileName);

            // Mover archivo
            File.Move(currentPath, newPath, overwrite: true);

            // Nueva URL pública

            var newUrl = $"/uploads/{targetFolder}/{fileName}";
            //var newUrl = $"{_settings.BaseUrl}/{targetFolder}/{fileName}";

            return (newPath, newUrl);
        }

    }
}
