using EXPERMIN.API.Infraestructure.Routes;
using EXPERMIN.CORE.Helpers;
using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.Portal.Banner;
using EXPERMIN.SERVICE.Dtos.User;
using EXPERMIN.SERVICE.Services.Portal.Interfaces;
using EXPERMIN.SERVICE.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXPERMIN.API.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN)]
    [Route(BannerApiRoute.BaseRoute)] // Se define la ruta base
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
        [HttpGet(BannerApiRoute.Task.GetAllBanners)]
        public async Task<ActionResult<List<BannerDto>>> GetAllBanners()
        {
            var banners = await _bannerService.GetAllBannerDatatable();

            return GetResultOrGenerateOperationError(banners);
        }
        [HttpGet(BannerApiRoute.Task.GetBannerById)]
        public async Task<ActionResult<BannerDto>> GetBannerById(Guid bannerId)
        {
            var banner = await _bannerService.GetBanner(bannerId);

            return GetResultOrGenerateOperationError(banner);
        }
        [HttpPost(BannerApiRoute.Task.RegisterBanner)]
        public async Task<ActionResult<BannerDto>> RegisterBanner([FromBody] UserRegisterDto model)
        {
            var userLoggedId = _userService.GetUserId();
            var operation = await _bannerService.InsertBanner(userLoggedId, model);

            return GetResultOrGenerateOperationError(operation);
        }
        [HttpPut(BannerApiRoute.Task.UpdateBanner)]
        public async Task<ActionResult<ResponseDto>> UpdateBanner(Guid bannerId, [FromBody] UserUpdateDto model)
        {
            var userLoggedId = _userService.GetUserId();
            var operation = await _bannerService.UpdateBanner(userLoggedId, bannerId, model);

            return GetResultOrGenerateOperationError(operation);
        }
        [HttpDelete(BannerApiRoute.Task.DeleteBanner)]
        public async Task<ActionResult<ResponseDto>> DeleteBanner(Guid bannerId)
        {
            var userLoggedId = _userService.GetUserId();
            var result = await _bannerService.DeleteBanner(bannerId, userLoggedId);

            return result.Result?.Suceso == true
                ? Ok(result.Result)
                : GenerateErrorOperation(result);
        }
    }
}
