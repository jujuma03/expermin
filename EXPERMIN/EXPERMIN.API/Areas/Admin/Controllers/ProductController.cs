using EXPERMIN.API.Areas.Admin.Infraestructure.Routes;
using EXPERMIN.API.Controllers;
using EXPERMIN.CORE.Helpers;
using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.Portal.Product;
using EXPERMIN.SERVICE.Services.Portal.Interfaces;
using EXPERMIN.SERVICE.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXPERMIN.API.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN)]
    [Route(ProductAdminApiRoute.BaseRoute)] // Se define la ruta base
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        public ProductController(IUserService userService, IProductService productService)
        {
            _userService = userService;
            _productService = productService;
        }
        [HttpGet(ProductAdminApiRoute.Task.GetAllProducts)]
        public async Task<ActionResult<List<ProductDto>>> GetAllProduct()
        {
            var userLoggedId = _userService.GetUserId();
            var products = await _productService.GetAllProducts(userLoggedId);

            return GetResultOrGenerateOperationError(products);
        }
        [HttpGet(ProductAdminApiRoute.Task.GetProductById)]
        public async Task<ActionResult<ProductDto>> GetProductById(Guid productId)
        {
            var userLoggedId = _userService.GetUserId();
            var products = await _productService.GetProduct(userLoggedId, productId);

            return GetResultOrGenerateOperationError(products);
        }
        [HttpPost(ProductAdminApiRoute.Task.RegisterProduct)]
        public async Task<ActionResult<ResponseDto>> RegisterProduct([FromBody] ProductRegisterDto model)
        {
            var userLoggedId = _userService.GetUserId();
            var operation = await _productService.InsertProduct(userLoggedId, model);

            return operation.Result?.Suceso == true
                ? Ok(operation.Result)
                : GenerateErrorOperation(operation);
        }
        [HttpPut(ProductAdminApiRoute.Task.UpdateProduct)]
        public async Task<ActionResult<ResponseDto>> UpdateProduct(Guid productId, [FromBody] ProductUpdateDto model)
        {
            var userLoggedId = _userService.GetUserId();
            var operation = await _productService.UpdateProduct(userLoggedId, productId, model);

            return operation.Result?.Suceso == true
                ? Ok(operation.Result)
                : GenerateErrorOperation(operation);
        }
        [HttpDelete(ProductAdminApiRoute.Task.DeleteProduct)]
        public async Task<ActionResult<ResponseDto>> DeleteProduct(Guid productId)
        {
            var userLoggedId = _userService.GetUserId();
            var result = await _productService.DeleteProduct(userLoggedId, productId);

            return result.Result?.Suceso == true
                ? Ok(result.Result)
                : GenerateErrorOperation(result);
        }
    }
}
