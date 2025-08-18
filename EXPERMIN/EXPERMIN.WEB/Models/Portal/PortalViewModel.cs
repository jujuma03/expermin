using EXPERMIN.WEB.Models.Portal.Banner;
using EXPERMIN.WEB.Models.Portal.Product;

namespace EXPERMIN.WEB.Models.Portal
{
    public class PortalViewModel
    {
        public List<BannerViewModel> Banner { get; set; }
        public List<ProductViewModel> Product { get; set; }
    }
}
