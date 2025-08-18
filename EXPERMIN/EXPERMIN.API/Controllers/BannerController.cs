using EXPERMIN.API.Infraestructure.Routes;
using EXPERMIN.CORE.Helpers;
using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.Portal.Banner;
using EXPERMIN.SERVICE.Dtos.User;
using EXPERMIN.SERVICE.Services.Portal.Interfaces;
using EXPERMIN.SERVICE.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static EXPERMIN.SERIVICE.Structs.Select2Structs;

namespace EXPERMIN.API.Controllers
{
    [Route(BannerApiRoute.BaseRoute)] // Se define la ruta base
    [ApiController]
    [AllowAnonymous]
    public class BannerController : BaseController
    {
        private readonly IBannerService _bannerService;
        public BannerController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }
        [HttpGet(BannerApiRoute.Task.GetAllBanners)]
        public async Task<ActionResult<List<BannerDto>>> GetAllBanners()
        {
            var banners = await _bannerService.GetAllBannersActive();

            return GetResultOrGenerateOperationError(banners);
        }
        [HttpGet(BannerApiRoute.Task.GetBannerById)]
        public async Task<ActionResult<BannerDto>> GetBannerById(Guid bannerId)
        {
            var banner = await _bannerService.GetBannerActive(bannerId);

            return GetResultOrGenerateOperationError(banner);
        }
    }
}
