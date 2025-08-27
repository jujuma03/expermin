using EXPERMIN.API.Areas.Admin.Infraestructure.Routes;
using EXPERMIN.API.Controllers;
using EXPERMIN.CORE.Helpers;
using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.Portal.Collaborator;
using EXPERMIN.SERVICE.Services.Portal.Interfaces;
using EXPERMIN.SERVICE.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXPERMIN.API.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN)]
    [Route(CollaboratorAdminApiRoute.BaseRoute)] // Se define la ruta base
    [ApiController]
    public class CollaboratorController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ICollaboratorService _collaboratorService;
        public CollaboratorController(IUserService userService, ICollaboratorService collaboratorService)
        {
            _userService = userService;
            _collaboratorService = collaboratorService;
        }
        [HttpGet(CollaboratorAdminApiRoute.Task.GetAllCollaborators)]
        public async Task<ActionResult<List<CollaboratorDto>>> GetAllCollaborators()
        {
            var userLoggedId = _userService.GetUserId();
            var collaborators = await _collaboratorService.GetAllCollaborators(userLoggedId);

            return GetResultOrGenerateOperationError(collaborators);
        }
        [HttpGet(CollaboratorAdminApiRoute.Task.GetCollaboratorById)]
        public async Task<ActionResult<CollaboratorDto>> GetCollaboratorById(Guid collaboratorId)
        {
            var userLoggedId = _userService.GetUserId();
            var collaborators = await _collaboratorService.GetCollaborator(userLoggedId, collaboratorId);

            return GetResultOrGenerateOperationError(collaborators);
        }
        [HttpPost(CollaboratorAdminApiRoute.Task.RegisterCollaborator)]
        public async Task<ActionResult<ResponseDto>> RegisterCollaborator([FromBody] CollaboratorRegisterDto model)
        {
            var userLoggedId = _userService.GetUserId();
            var operation = await _collaboratorService.InsertCollaborator(userLoggedId, model);

            return operation.Result?.Suceso == true
                ? Ok(operation.Result)
                : GenerateErrorOperation(operation);
        }
        [HttpPut(CollaboratorAdminApiRoute.Task.UpdateCollaborator)]
        public async Task<ActionResult<ResponseDto>> UpdateCollaborator(Guid collaboratorId, [FromBody] CollaboratorUpdateDto model)
        {
            var userLoggedId = _userService.GetUserId();
            var operation = await _collaboratorService.UpdateCollaborator(userLoggedId, collaboratorId, model);

            return operation.Result?.Suceso == true
                ? Ok(operation.Result)
                : GenerateErrorOperation(operation);
        }
        [HttpDelete(CollaboratorAdminApiRoute.Task.DeleteCollaborator)]
        public async Task<ActionResult<ResponseDto>> DeleteCollaborator(Guid collaboratorId)
        {
            var userLoggedId = _userService.GetUserId();
            var result = await _collaboratorService.DeleteCollaborator(userLoggedId, collaboratorId);

            return result.Result?.Suceso == true
                ? Ok(result.Result)
                : GenerateErrorOperation(result);
        }
    }
}
