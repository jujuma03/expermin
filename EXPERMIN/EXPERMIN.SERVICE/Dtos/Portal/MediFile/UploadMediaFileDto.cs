using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Dtos.Portal.MediFile
{
    public class UploadMediaFileDto
    {
        [Required(ErrorMessage = "La imagen es requerida")]
        public IFormFile File { get; set; }
    }
}
