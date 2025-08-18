using EXPERMIN.API.Areas.Admin.Infraestructure.Routes;
using EXPERMIN.API.Controllers;
using EXPERMIN.API.Infraestructure.Routes;
using EXPERMIN.CORE.Helpers;
using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.Portal.Banner;
using EXPERMIN.SERVICE.Services.Portal.Interfaces;
using EXPERMIN.SERVICE.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXPERMIN.API.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN)]
    [Route(BannerAdminApiRoute.BaseRoute)] // Se define la ruta base
    [ApiController]
    public class BannerController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IBannerService _bannerService;
        public BannerController(IUserService userService, IBannerService bannerService)
        {
            _userService = userService;
            _bannerService = bannerService;
        }
        [HttpGet(BannerAdminApiRoute.Task.GetAllBanners)]
        public async Task<ActionResult<List<BannerDto>>> GetAllBanner()
        {
            var userLoggedId = _userService.GetUserId();
            var banners = await _bannerService.GetAllBanners(userLoggedId);

            return GetResultOrGenerateOperationError(banners);
        }
        [HttpGet(BannerAdminApiRoute.Task.GetBannerById)]
        public async Task<ActionResult<BannerDto>> GetBannerById(Guid bannerId)
        {
            var userLoggedId = _userService.GetUserId();
            var banners = await _bannerService.GetBanner(userLoggedId, bannerId);

            return GetResultOrGenerateOperationError(banners);
        }
        [HttpPost(BannerAdminApiRoute.Task.RegisterBanner)]
        public async Task<ActionResult<ResponseDto>> RegisterBanner([FromBody] BannerRegisterDto model)
        {
            var userLoggedId = _userService.GetUserId();
            var operation = await _bannerService.InsertBanner(userLoggedId, model);

            return operation.Result?.Suceso == true
                ? Ok(operation.Result)
                : GenerateErrorOperation(operation);
        }
        [HttpPut(BannerAdminApiRoute.Task.UpdateBanner)]
        public async Task<ActionResult<ResponseDto>> UpdateBanner(Guid bannerId, [FromBody] BannerUpdateDto model)
        {
            var userLoggedId = _userService.GetUserId();
            var operation = await _bannerService.UpdateBanner(userLoggedId, bannerId, model);

            return operation.Result?.Suceso == true
                ? Ok(operation.Result)
                : GenerateErrorOperation(operation);
        }
        [HttpDelete(BannerAdminApiRoute.Task.DeleteBanner)]
        public async Task<ActionResult<ResponseDto>> DeleteBanner(Guid bannerId)
        {
            var userLoggedId = _userService.GetUserId();
            var result = await _bannerService.DeleteBanner(userLoggedId, bannerId);

            return result.Result?.Suceso == true
                ? Ok(result.Result)
                : GenerateErrorOperation(result);
        }
    }
}
