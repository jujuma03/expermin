using EXPERMIN.API.Infraestructure.Routes;
using EXPERMIN.SERVICE.Dtos.Portal.Testimony;
using EXPERMIN.SERVICE.Services.Portal.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXPERMIN.API.Controllers
{
    [Route(TestimonyApiRoute.BaseRoute)] // Se define la ruta base
    [ApiController]
    [AllowAnonymous]
    public class TestimonyController : BaseController
    {
        private readonly ITestimonyService _testimonyService;
        public TestimonyController(ITestimonyService testimonyService)
        {
            _testimonyService = testimonyService;
        }
        [HttpGet(TestimonyApiRoute.Task.GetAllTestimonies)]
        public async Task<ActionResult<List<TestimonyDto>>> GetAllTestimonies()
        {
            var testimonys = await _testimonyService.GetAllTestimoniesActive();

            return GetResultOrGenerateOperationError(testimonys);
        }
        [HttpGet(TestimonyApiRoute.Task.GetTestimonyById)]
        public async Task<ActionResult<TestimonyDto>> GetTestimonyById(Guid testimonyId)
        {
            var testimony = await _testimonyService.GetTestimonyActive(testimonyId);

            return GetResultOrGenerateOperationError(testimony);
        }
    }
}
