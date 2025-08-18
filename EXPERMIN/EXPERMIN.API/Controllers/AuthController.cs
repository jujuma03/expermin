using EXPERMIN.API.Infraestructure.Routes;
using EXPERMIN.CORE.Helpers;
using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.User;
using EXPERMIN.SERVICE.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXPERMIN.API.Controllers
{
    [Route(AuthApiRoute.BaseRoute)] // Se define la ruta base
    [ApiController]
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userServices;
        public AuthController(IAuthService authService, IUserService userServices)
        {
            _authService = authService;
            _userServices = userServices;
        }

        [HttpPost(AuthApiRoute.Task.Login)]
        public async Task<ActionResult<UserLoginResponseDto>> Login([FromBody] UserLoginDto model)
        {
            var validationError = GenerateErrorIfNoJsonBody(model);
            if (validationError is BadRequestObjectResult)
                return validationError;

            var operation = await _authService.Login(model);
            return GetResultOrGenerateOperationError(operation);
        }
    }
}
