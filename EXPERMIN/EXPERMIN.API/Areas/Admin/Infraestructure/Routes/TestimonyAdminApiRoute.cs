namespace EXPERMIN.API.Areas.Admin.Infraestructure.Routes
{
    public class TestimonyAdminApiRoute
    {
        public const string BaseRoute = "api/v1/admin/testimony";
        public static class Task
        {
            public const string GetAllTestimonies = "get-all";
            public const string GetTestimonyById = "get/{testimonyId:guid}";
            public const string RegisterTestimony = "register";
            public const string UpdateTestimony = "update/{testimonyId:guid}";
            public const string DeleteTestimony = "delete/{testimonyId:guid}";
        }
    }
}
