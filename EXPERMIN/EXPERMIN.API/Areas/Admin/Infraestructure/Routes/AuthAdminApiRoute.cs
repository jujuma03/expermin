namespace EXPERMIN.API.Areas.Admin.Infraestructure.Routes
{
    public class AuthAdminApiRoute
    {
        public const string BaseRoute = "api/v1/admin/auth";
        public static class Task
        {
            public const string RegisterAccountAdmin = "register-account";
            public const string Logout = "logout";
        }
    }
}
