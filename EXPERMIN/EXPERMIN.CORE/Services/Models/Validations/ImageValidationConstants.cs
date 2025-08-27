using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.CORE.Services.Models.Validations
{
    public static class ImageValidationConstants
    {
        // Extensiones permitidas
        public static readonly string[] AllowedExtensions = new[]
        {
            ".jpg", ".jpeg",   // JPEG
            ".png",            // PNG
            ".gif",            // GIF
            ".webp",           // WEBP
            ".bmp",            // BMP
            ".tif", ".tiff",   // TIFF
            ".svg",            // SVG
            ".ico",            // ICO
            ".heif", ".heic",  // HEIF/HEIC
            ".avif"            // AVIF
        };

        // MIME types permitidos
        public static readonly string[] AllowedMimeTypes = new[]
        {
            "image/jpeg",
            "image/png",
            "image/gif",
            "image/webp",
            "image/bmp",
            "image/tiff",
            "image/svg+xml",
            "image/x-icon",
            "image/vnd.microsoft.icon",
            "image/heif",
            "image/heic",
            "image/avif"
        };

        // Tamaños máximos (en bytes)
        public const long MaxFileSize = 5 * 1024 * 1024; // 5 MB
        public const long MinFileSize = 5 * 1024;       // 10 KB (evita archivos vacíos o muy pequeños)
    }
}
