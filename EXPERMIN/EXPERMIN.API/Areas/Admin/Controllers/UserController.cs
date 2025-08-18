using EXPERMIN.API.Areas.Admin.Infraestructure.Routes;
using EXPERMIN.API.Controllers;
using EXPERMIN.CORE.Helpers;
using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.User;
using EXPERMIN.SERVICE.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXPERMIN.API.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN)]
    [Route(UserApiRoute.BaseRoute)] // Se define la ruta base
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userServices;
        public UserController(IUserService userServices)
        {
            _userServices = userServices;
        }
        [HttpGet(UserApiRoute.Task.GetAllUsers)]
        public async Task<ActionResult<List<UserDetailDto>>> GetAllUsers()
        {
            var userLoggedId = _userServices.GetUserId();
            var users = await _userServices.GetAllUser(userLoggedId);

            return GetResultOrGenerateOperationError(users);
        }
        [HttpGet(UserApiRoute.Task.GetUserById)]
        public async Task<ActionResult<UserDetailDto>> GetUserById(string userId)
        {
            var userLoggedId = _userServices.GetUserId();
            var user = await _userServices.GetUserById(userLoggedId, userId);

            return GetResultOrGenerateOperationError(user);
        }
        [HttpPost(UserApiRoute.Task.RegistrarUser)]
        public async Task<ActionResult<UserDto>> RegistrarUser([FromBody] UserRegisterDto model)
        {
            var userLoggedId = _userServices.GetUserId();
            var operation = await _userServices.InsertUser(userLoggedId, model);

            return GetResultOrGenerateOperationError(operation);
        }
        [HttpPut(UserApiRoute.Task.UpdateUser)]
        public async Task<ActionResult<UserDetailDto>> UpdateUser(string userId, [FromBody] UserUpdateDto model)
        {
            var userLoggedId = _userServices.GetUserId();
            var operation = await _userServices.UpdateUser(userLoggedId, userId, model);

            return GetResultOrGenerateOperationError(operation);
        }
        [HttpDelete(UserApiRoute.Task.DeleteUser)]
        public async Task<ActionResult<ResponseDto>> DeleteUser(string userId)
        {
            var userLoggedId = _userServices.GetUserId();
            var result = await _userServices.DeleteUser(userId, userLoggedId);

            return result.Result?.Suceso == true
                ? Ok(result.Result)
                : GenerateErrorOperation(result);
        }
    }
}
