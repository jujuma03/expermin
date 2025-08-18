namespace EXPERMIN.API.Areas.Admin.Infraestructure.Routes
{
    public class ProductAdminApiRoute
    {
        public const string BaseRoute = "api/v1/admin/product";
        public static class Task
        {
            public const string GetAllProducts = "get-all";
            public const string GetProductById = "get/{productId:guid}";
            public const string RegisterProduct = "register";
            public const string UpdateProduct = "update/{productId:guid}";
            public const string DeleteProduct = "delete/{productId:guid}";
        }
    }
}
