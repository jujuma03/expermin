using EXPERMIN.CORE.Helpers;
using EXPERMIN.WEB.Extensions;
using EXPERMIN.WEB.Models.Portal;
using EXPERMIN.WEB.Models.Portal.Banner;
using EXPERMIN.WEB.Models.Portal.Product;
using EXPERMIN.WEB.Services.Portal.Portal.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EXPERMIN.WEB.Controllers
{
    public class PortalController : Controller
    {
        private readonly IPortalService _portalService;
        public PortalController(IPortalService portalService)
        {
            _portalService = portalService;
        }
        public async Task<IActionResult> Index()
        {
            var banners = await _portalService.GetAllBannersActiveAsync();
            var model = new PortalViewModel
            {
                Banner = banners ?? new List<BannerViewModel>()
            };
            return View(model);
        }
        [HttpGet("get-products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _portalService.GetAllProductsActiveAsync();
            var view = await this.RenderViewToStringAsync(@"/Views/Portal/Partials/_Products.cshtml", products);
            return Ok(view);
        }
    }
}
