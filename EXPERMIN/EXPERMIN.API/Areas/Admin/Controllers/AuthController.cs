using Azure.Core;
using EXPERMIN.API.Areas.Admin.Infraestructure.Routes;
using EXPERMIN.API.Controllers;
using EXPERMIN.API.Infraestructure.Routes;
using EXPERMIN.CORE.Helpers;
using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.User;
using EXPERMIN.SERVICE.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXPERMIN.API.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN)]
    [Route(AuthAdminApiRoute.BaseRoute)] // Se define la ruta base
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userServices;
        public AuthController(IAuthService authService, IUserService userServices)
        {
            _authService = authService;
            _userServices = userServices;
        }

        [HttpPost(AuthAdminApiRoute.Task.RegisterAccountAdmin)]
        public async Task<ActionResult<UserDto>> RegisterAccountAdmin([FromBody] UserRegisterDto model)
        {
            var validationError = GenerateErrorIfNoJsonBody(model);
            if (validationError is BadRequestObjectResult) return validationError;

            var operation = await _authService.RegisterAccountAdmin(model);
            return GetResultOrGenerateOperationError(operation);
        }
        [HttpPost(AuthAdminApiRoute.Task.Logout)]
        public async Task<ActionResult<ResponseDto>> Logout()
        {
            var userLoggedId = _userServices.GetUserId();
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            var result = await _authService.Logout(token, userLoggedId);
            return GetResultOrGenerateOperationError(result);
        }
    }
}
