using EXPERMIN.API.Areas.Admin.Infraestructure.Routes;
using EXPERMIN.API.Controllers;
using EXPERMIN.CORE.Helpers;
using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.Portal.Testimony;
using EXPERMIN.SERVICE.Services.Portal.Interfaces;
using EXPERMIN.SERVICE.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXPERMIN.API.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN)]
    [Route(TestimonyAdminApiRoute.BaseRoute)] // Se define la ruta base
    [ApiController]
    public class TestimonyController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ITestimonyService _testimonyService;
        public TestimonyController(IUserService userService, ITestimonyService testimonyService)
        {
            _userService = userService;
            _testimonyService = testimonyService;
        }
        [HttpGet(TestimonyAdminApiRoute.Task.GetAllTestimonies)]
        public async Task<ActionResult<List<TestimonyDto>>> GetAllTestimonies()
        {
            var userLoggedId = _userService.GetUserId();
            var testimonies = await _testimonyService.GetAllTestimonies(userLoggedId);

            return GetResultOrGenerateOperationError(testimonies);
        }
        [HttpGet(TestimonyAdminApiRoute.Task.GetTestimonyById)]
        public async Task<ActionResult<TestimonyDto>> GetTestimonyById(Guid testimonyId)
        {
            var userLoggedId = _userService.GetUserId();
            var testimonys = await _testimonyService.GetTestimony(userLoggedId, testimonyId);

            return GetResultOrGenerateOperationError(testimonys);
        }
        [HttpPost(TestimonyAdminApiRoute.Task.RegisterTestimony)]
        public async Task<ActionResult<ResponseDto>> RegisterTestimony([FromBody] TestimonyRegisterDto model)
        {
            var userLoggedId = _userService.GetUserId();
            var operation = await _testimonyService.InsertTestimony(userLoggedId, model);

            return operation.Result?.Suceso == true
                ? Ok(operation.Result)
                : GenerateErrorOperation(operation);
        }
        [HttpPut(TestimonyAdminApiRoute.Task.UpdateTestimony)]
        public async Task<ActionResult<ResponseDto>> UpdateTestimony(Guid testimonyId, [FromBody] TestimonyUpdateDto model)
        {
            var userLoggedId = _userService.GetUserId();
            var operation = await _testimonyService.UpdateTestimony(userLoggedId, testimonyId, model);

            return operation.Result?.Suceso == true
                ? Ok(operation.Result)
                : GenerateErrorOperation(operation);
        }
        [HttpDelete(TestimonyAdminApiRoute.Task.DeleteTestimony)]
        public async Task<ActionResult<ResponseDto>> DeleteTestimony(Guid testimonyId)
        {
            var userLoggedId = _userService.GetUserId();
            var result = await _testimonyService.DeleteTestimony(userLoggedId, testimonyId);

            return result.Result?.Suceso == true
                ? Ok(result.Result)
                : GenerateErrorOperation(result);
        }
    }
}
