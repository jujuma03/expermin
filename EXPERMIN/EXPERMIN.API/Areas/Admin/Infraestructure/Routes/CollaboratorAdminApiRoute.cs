namespace EXPERMIN.API.Areas.Admin.Infraestructure.Routes
{
    public class CollaboratorAdminApiRoute
    {
        public const string BaseRoute = "api/v1/admin/collaborator";
        public static class Task
        {
            public const string GetAllCollaborators = "get-all";
            public const string GetCollaboratorById = "get/{collaboratorId:guid}";
            public const string RegisterCollaborator = "register";
            public const string UpdateCollaborator = "update/{collaboratorId:guid}";
            public const string DeleteCollaborator = "delete/{collaboratorId:guid}";
        }
    }
}
