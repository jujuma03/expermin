using EXPERMIN.WEB.Models.Portal.Banner;
using EXPERMIN.WEB.Models.Portal.Collaborator;
using EXPERMIN.WEB.Models.Portal.Product;
using EXPERMIN.WEB.Models.Portal.Testimony;

namespace EXPERMIN.WEB.Services.Portal.Portal.Interfaces
{
    public interface IPortalService
    {
        Task<List<BannerViewModel>> GetAllBannersActiveAsync();
        Task<List<ProductViewModel>> GetAllProductsActiveAsync();
		Task<List<TestimonyViewModel>> GetAllTestimonysActiveAsync();
        Task<List<CollaboratorViewModel>> GetAllCollaboratorsActiveAsync();

    }
}
