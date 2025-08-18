using EXPERMIN.API.Infraestructure.Routes;
using EXPERMIN.SERVICE.Dtos.Portal.Product;
using EXPERMIN.SERVICE.Services.Portal.Interfaces;
using EXPERMIN.SERVICE.Services.User.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXPERMIN.API.Controllers
{
    [Route(ProductApiRoute.BaseRoute)] // Se define la ruta base
    [ApiController]
    [AllowAnonymous]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet(ProductApiRoute.Task.GetAllProducts)]
        public async Task<ActionResult<List<ProductDto>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsActive();

            return GetResultOrGenerateOperationError(products);
        }
        [HttpGet(ProductApiRoute.Task.GetProductById)]
        public async Task<ActionResult<ProductDto>> GetProductById(Guid productId)
        {
            var product = await _productService.GetProductActive(productId);

            return GetResultOrGenerateOperationError(product);
        }
    }
}
