using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.Portal.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Services.Portal.Interfaces
{
    public interface IProductService
    {
        Task<OperationDto<List<ProductDto>>> GetAllProducts(string userLoggedId);
        Task<OperationDto<List<ProductDto>>> GetAllProductsActive();
        Task<OperationDto<ProductDto>> GetProduct(string userLoggedId, Guid id);
        Task<OperationDto<ProductDto>> GetProductActive(Guid id);
        Task<OperationDto<ResponseDto>> InsertProduct(string userLoggedId, ProductRegisterDto model);
        Task<OperationDto<ResponseDto>> UpdateProduct(string userLoggedId, Guid productId, ProductUpdateDto model);
        Task<OperationDto<ResponseDto>> DeleteProduct(string userLoggedId, Guid productId);
    }
}
