using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Dtos.Portal.MediFile
{
    public class MediaFileDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("fileName")]
        public string FileName { get; set; }     // nombre original

        [JsonPropertyName("path")] 
        public string Path { get; set; }         // ruta en disco/S3

        [JsonPropertyName("url")] 
        public string Url { get; set; }          // URL pública

        [JsonPropertyName("contentType")] 
        public string ContentType { get; set; }  // image/png, video/mp4, etc.

        [JsonPropertyName("size")] 
        public long Size { get; set; }           // en bytes

        [JsonPropertyName("uploadDate")] 
        public string UploadDate { get; set; }

        [JsonPropertyName("isTemporary")] 
        public bool IsTemporary { get; set; }    // true = aún no referenciado

    }
}
