namespace EXPERMIN.API.Areas.Admin.Infraestructure.Routes
{
    public class UserApiRoute
    {
        public const string BaseRoute = "api/v1/admin/user";
        public static class Task
        {
            public const string GetAllUsers = "get-all";
            public const string GetUserById = "get/{userId:guid}";
            public const string RegistrarUser = "register";
            public const string UpdateUser = "update/{userId:guid}";
            public const string DeleteUser = "delete/{userId:guid}";
        }
    }
}
