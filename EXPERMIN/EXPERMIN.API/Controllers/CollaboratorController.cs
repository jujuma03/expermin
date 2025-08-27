using EXPERMIN.API.Infraestructure.Routes;
using EXPERMIN.SERVICE.Dtos.Portal.Collaborator;
using EXPERMIN.SERVICE.Services.Portal.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXPERMIN.API.Controllers
{
    [Route(CollaboratorApiRoute.BaseRoute)] // Se define la ruta base
    [ApiController]
    [AllowAnonymous]
    public class CollaboratorController : BaseController
    {
        private readonly ICollaboratorService _collaboratorService;
        public CollaboratorController(ICollaboratorService collaboratorService)
        {
            _collaboratorService = collaboratorService;
        }
        [HttpGet(CollaboratorApiRoute.Task.GetAllCollaborators)]
        public async Task<ActionResult<List<CollaboratorDto>>> GetAllCollaborators()
        {
            var collaborators = await _collaboratorService.GetAllCollaboratorsActive();

            return GetResultOrGenerateOperationError(collaborators);
        }
        [HttpGet(CollaboratorApiRoute.Task.GetCollaboratorById)]
        public async Task<ActionResult<CollaboratorDto>> GetCollaboratorById(Guid collaboratorId)
        {
            var collaborator = await _collaboratorService.GetCollaboratorActive(collaboratorId);

            return GetResultOrGenerateOperationError(collaborator);
        }
    }
}
