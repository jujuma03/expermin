namespace EXPERMIN.API.Infraestructure.Routes
{
    public class CollaboratorApiRoute
    {
        public const string BaseRoute = "api/v1/collaborator";
        public static class Task
        {
            public const string GetAllCollaborators = "get-all";
            public const string GetCollaboratorById = "get/{collaboratorId:guid}";
        }
    }
}
