namespace EXPERMIN.API.Infraestructure.Routes
{
    public class ProductApiRoute
    {
        public const string BaseRoute = "api/v1/produtc";
        public static class Task
        {
            public const string GetAllProducts = "get-all";
            public const string GetProductById = "get/{produtcId:guid}";
        }
    }
}
