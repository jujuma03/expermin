using EXPERMIN.WEB.Models.Portal.Banner;
using EXPERMIN.WEB.Models.Portal.Product;

namespace EXPERMIN.WEB.Services.Portal.Portal.Interfaces
{
    public interface IPortalService
    {
        Task<List<BannerViewModel>> GetAllBannersActiveAsync();
        Task<List<ProductViewModel>> GetAllProductsActiveAsync();
    }
}
