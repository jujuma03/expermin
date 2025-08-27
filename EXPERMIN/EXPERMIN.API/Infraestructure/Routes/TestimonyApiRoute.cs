namespace EXPERMIN.API.Infraestructure.Routes
{
    public class TestimonyApiRoute
    {
        public const string BaseRoute = "api/v1/testimony";
        public static class Task
        {
            public const string GetAllTestimonies = "get-all";
            public const string GetTestimonyById = "get/{testimonyId:guid}";
        }
    }
}
