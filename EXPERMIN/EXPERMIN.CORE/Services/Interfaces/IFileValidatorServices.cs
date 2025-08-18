using EXPERMIN.CORE.Services.Models.Generic;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.CORE.Services.Interfaces
{
    public interface IFileValidatorServices
    {
        ResponseCore ValidateImage(IFormFile file);
    }
}
