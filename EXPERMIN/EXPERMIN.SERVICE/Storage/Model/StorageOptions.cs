using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Storage.Model
{
    public class StorageOptions
    {
        public string BasePath { get; set; } = string.Empty; // Ej: C:\...\upload
        public string BaseUrl { get; set; } = string.Empty;  // Ej: https://localhost:7020/uploads
    }

}
