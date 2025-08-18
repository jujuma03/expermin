namespace EXPERMIN.API.Infraestructure.Routes
{
    public class BannerApiRoute
    {
        public const string BaseRoute = "api/v1/banner";
        public static class Task
        {
            public const string GetAllBanners = "get-all";
            public const string GetBannerById = "get/{bannerId:guid}";
        }
    }
}
