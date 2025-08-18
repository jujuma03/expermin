using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.ENTITIES.Models
{
    public class MediaFile : BaseEntity
    {
        public string FileName { get; set; }     // nombre original
        public string Path { get; set; }         // ruta en disco/S3
        public string Url { get; set; }          // URL pública
        public string ContentType { get; set; }  // image/png, video/mp4, etc.
        public long Size { get; set; }           // en bytes
        public DateTime UploadDate { get; set; }
        public bool IsTemporary { get; set; }    // true = aún no referenciado
    }
}
