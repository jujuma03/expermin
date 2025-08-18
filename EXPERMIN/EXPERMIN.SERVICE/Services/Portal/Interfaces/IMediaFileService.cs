using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.Portal.MediFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Services.Portal.Interfaces
{
    public interface IMediaFileService
    {
        Task<OperationDto<MediaFileDto>> Upload(string userId, UploadMediaFileDto dto);
    }
}
