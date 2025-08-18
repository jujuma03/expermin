namespace EXPERMIN.API.Areas.Admin.Infraestructure.Routes
{
    public class BannerAdminApiRoute
    {
        public const string BaseRoute = "api/v1/admin/banner";
        public static class Task
        {
            public const string GetAllBanners = "get-all";
            public const string GetBannerById = "get/{bannerId:guid}";
            public const string RegisterBanner = "register";
            public const string UpdateBanner = "update/{bannerId:guid}";
            public const string DeleteBanner = "delete/{bannerId:guid}";
        }
    }
}
