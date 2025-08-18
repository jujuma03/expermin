using EXPERMIN.API.Areas.Admin.Infraestructure.Routes;
using EXPERMIN.API.Controllers;
using EXPERMIN.CORE.Helpers;
using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.Portal.Banner;
using EXPERMIN.SERVICE.Dtos.Portal.MediFile;
using EXPERMIN.SERVICE.Services.Portal.Interfaces;
using EXPERMIN.SERVICE.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXPERMIN.API.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN)]
    [Route(MediaFileApiRoute.BaseRoute)] // Se define la ruta base
    [ApiController]
    public class MediaFileController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMediaFileService _mediaFileService;
        public MediaFileController(IUserService userService, IMediaFileService mediaFileService )
        {
            _userService = userService;
            _mediaFileService = mediaFileService;
        }
        [HttpPost(MediaFileApiRoute.Task.UploadMediaFile)]
        public async Task<ActionResult<MediaFileDto>> UploadMediaFile([FromForm] UploadMediaFileDto model)
        {
            var userLoggedId = _userService.GetUserId();
            var operation = await _mediaFileService.Upload(userLoggedId, model);

            return GetResultOrGenerateOperationError(operation);
        }
    }
}
