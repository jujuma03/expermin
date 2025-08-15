namespace EXPERMIN.API.Infraestructure.Routes
{
    public class BannerApiRoute
    {
        public const string BaseRoute = "api/v1/banner";
        public static class Task
        {
            public const string GetAllBanners = "get-banners";
            public const string GetBannerById = "get-banner/{bannerId:guid}";
            public const string RegisterBanner = "register-banner";
            public const string UpdateBanner = "update-banner/{bannerId:guid}";
            public const string DeleteBanner = "delete-banner/{bannerId:guid}";
        }
    }
}
