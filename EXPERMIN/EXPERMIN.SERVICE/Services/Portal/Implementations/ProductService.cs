using AutoMapper;
using EXPERMIN.CORE.Extensions;
using EXPERMIN.CORE.Helpers;
using EXPERMIN.ENTITIES.Models;
using EXPERMIN.REPOSITORY.Repositories.Portal.Implementations;
using EXPERMIN.REPOSITORY.Repositories.Portal.Interfaces;
using EXPERMIN.REPOSITORY.Repositories.Storage.Interfaces;
using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.Portal.MediFile;
using EXPERMIN.SERVICE.Dtos.Portal.Product;
using EXPERMIN.SERVICE.Services.Portal.Interfaces;
using EXPERMIN.SERVICE.Services.User.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EXPERMIN.CORE.Helpers.ConstantHelpers;

namespace EXPERMIN.SERVICE.Services.Portal.Implementations
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserValidationService _userValidationService;
        private readonly IMapper _mapper;
        private readonly IMediaFileRepository _mediaFileRepository;
        public ProductService(IProductRepository productRepository, IUserValidationService userValidationService, IMapper mapper,
            IMediaFileRepository mediaFileRepository)
        {
            _productRepository = productRepository;
            _userValidationService = userValidationService;
            _mapper = mapper;
            _mediaFileRepository = mediaFileRepository;
        }

        public async Task<OperationDto<List<ProductDto>>> GetAllProductsActive()
        {
            var products = await _productRepository
                .GetAsQueryable()
                .AsNoTracking()
                .Where(x => x.Status == ConstantHelpers.BANNER.STATUS.ACTIVE)
                .Select(x => new ProductDto
                {
                    Id = x.Id.ToString(),
                    Title = x.Title,
                    PublicationDate = x.PublicationDate.ToLocalDateTimeFormat(),
                    Status = ConstantHelpers.BANNER.STATUS.VALUES[x.Status],
                    Order = x.Order ?? 0,
                    Description = x.Description,
                    Image = x.MediaFile == null ? null : new MediaFileDto
                    {
                        Id = x.MediaFile.Id,
                        FileName = x.MediaFile.FileName,
                        Path = x.MediaFile.Path,
                        Url = x.MediaFile.Url,
                        ContentType = x.MediaFile.ContentType,
                        Size = x.MediaFile.Size,
                        UploadDate = x.MediaFile.UploadDate.ToLocalDateTimeFormat(),
                        IsTemporary = x.MediaFile.IsTemporary
                    }
                })
                .ToListAsync();

            if (!products.Any())
                return new OperationDto<List<ProductDto>>(OperationCodeDto.EmptyResult, "No hay productos activos.");

            return new OperationDto<List<ProductDto>>(products);
        }
        public async Task<OperationDto<List<ProductDto>>> GetAllProducts(string userLoggedId)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<List<ProductDto>>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            var products = await _productRepository
                .GetAsQueryable()
                .Select(x => new ProductDto
                {
                    Id = x.Id.ToString(),
                    Title = x.Title,
                    PublicationDate = x.PublicationDate.ToLocalDateTimeFormat(),
                    Status = ConstantHelpers.BANNER.STATUS.VALUES[x.Status],
                    Order = x.Order ?? 0,
                    Description = x.Description,
                    Image = x.MediaFile == null ? null : new MediaFileDto
                    {
                        Id = x.MediaFile.Id,
                        FileName = x.MediaFile.FileName,
                        Path = x.MediaFile.Path,
                        Url = x.MediaFile.Url,
                        ContentType = x.MediaFile.ContentType,
                        Size = x.MediaFile.Size,
                        UploadDate = x.MediaFile.UploadDate.ToLocalDateTimeFormat(),
                        IsTemporary = x.MediaFile.IsTemporary
                    }
                })
                .ToListAsync();

            if (!products.Any())
                return new OperationDto<List<ProductDto>>(OperationCodeDto.EmptyResult, "No hay productos activos.");

            return new OperationDto<List<ProductDto>>(products);
        }
        public async Task<OperationDto<ProductDto>> GetProduct(string userLoggedId, Guid id)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<ProductDto>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            if (id == Guid.Empty)
                return new OperationDto<ProductDto>(OperationCodeDto.InvalidParameters, "El ID es inválido.");

            var product = await _productRepository.GetProductDetail(id);

            if (product == null)
                return new OperationDto<ProductDto>(OperationCodeDto.DoesNotExist, "El producto no existe.");

            var dto = new ProductDto
            {
                Id = product.Id.ToString(),
                Title = product.Title,
                PublicationDate = product.PublicationDate.ToLocalDateTimeFormat(),
                Status = ConstantHelpers.BANNER.STATUS.VALUES[product.Status],
                Order = product.Order ?? 0,
                Description = product.Description,
                Image = product.MediaFile == null ? null : new MediaFileDto
                {
                    Id = product.MediaFile.Id,
                    FileName = product.MediaFile.FileName,
                    Path = product.MediaFile.Path,
                    Url = product.MediaFile.Url,
                    ContentType = product.MediaFile.ContentType,
                    Size = product.MediaFile.Size,
                    UploadDate = product.MediaFile.UploadDate.ToLocalDateTimeFormat(),
                    IsTemporary = product.MediaFile.IsTemporary
                }
            };

            return new OperationDto<ProductDto>(dto);
        }
        public async Task<OperationDto<ProductDto>> GetProductActive(Guid id)
        {
            if (id == Guid.Empty)
                return new OperationDto<ProductDto>(OperationCodeDto.InvalidParameters, "El ID es inválido.");

            var product = await _productRepository.GetProductDetail(id);

            if (product == null || product.Status == ConstantHelpers.BANNER.STATUS.HIDDEN)
                return new OperationDto<ProductDto>(OperationCodeDto.DoesNotExist, "El producto no existe.");

            var dto = new ProductDto
            {
                Id = product.Id.ToString(),
                Title = product.Title,
                PublicationDate = product.PublicationDate.ToLocalDateTimeFormat(),
                Status = ConstantHelpers.BANNER.STATUS.VALUES[product.Status],
                Order = product.Order ?? 0,
                Description = product.Description,
                Image = product.MediaFile == null ? null : new MediaFileDto
                {
                    Id = product.MediaFile.Id,
                    FileName = product.MediaFile.FileName,
                    Path = product.MediaFile.Path,
                    Url = product.MediaFile.Url,
                    ContentType = product.MediaFile.ContentType,
                    Size = product.MediaFile.Size,
                    UploadDate = product.MediaFile.UploadDate.ToLocalDateTimeFormat(),
                    IsTemporary = product.MediaFile.IsTemporary
                }
            };

            return new OperationDto<ProductDto>(dto);
        }
        public async Task<OperationDto<ResponseDto>> InsertProduct(string userLoggedId, ProductRegisterDto model)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<ResponseDto>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            if (model == null)
                return new OperationDto<ResponseDto>(OperationCodeDto.InvalidParameters, "El product no puede ser nulo.");

            // Validación de imágenes obligatorias (ejemplo: al menos 1 archivo)
            if (model.ImageId == Guid.Empty)
                return new OperationDto<ResponseDto>(OperationCodeDto.InvalidParameters, "Debe adjuntar una imagen.");

            // Obtener imagen
            var image = await _mediaFileRepository.Get(model.ImageId);
            if (image == null)
                return new OperationDto<ResponseDto>(OperationCodeDto.DoesNotExist, "No existe la imagen adjuntada al product.");


            var product = new Product()
            {
                Description = model.Description,
                Title = model.Title,
                Status = (byte)model.Status,
                PublicationDate = DateTime.UtcNow,
                Order = (byte)model.Order,
                // Mapeo de archivos asociados al product
                MediaFileId = image.Id
            };

            // --- Transacción ---
            return await _productRepository.ExecuteInTransactionWithSaveAsync(async () =>
            {
                await _productRepository.Add(product);

                // Actualizar imagen
                image.IsTemporary = false;
                _mediaFileRepository.Attach(image);

                return new OperationDto<ResponseDto>(
                    new ResponseDto { Suceso = true, Mensaje = "Se ha insertado el producto correctamente." });
            });
        }
        public async Task<OperationDto<ResponseDto>> UpdateProduct(string userLoggedId, Guid productId, ProductUpdateDto model)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<ResponseDto>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            if (model == null)
                return new OperationDto<ResponseDto>(OperationCodeDto.InvalidParameters, "El producto no puede ser nulo.");

            // Buscar product
            var product = await _productRepository.GetProductDetail(productId);
            if (product == null)
                return new OperationDto<ResponseDto>(OperationCodeDto.DoesNotExist, "El producto no existe.");

            
            // Si quiere ocultarlo, colocar la orden en 0 por defecto
            if (model.Status == ConstantHelpers.BANNER.STATUS.HIDDEN)
                model.Order = 0;

            Guid? oldImageId = null;

            // Mapear resto de propiedades (menos la imagen, que manejamos manualmente)
            _mapper.Map(model, product);

            // Si viene imagen nueva diferente a la actual → swap
            if (model.ImageId.HasValue && model.ImageId.Value != product.MediaFileId)
            {
                // Verificar que el nuevo MediaFile exista
                var newImage = await _mediaFileRepository.Get(model.ImageId.Value);
                if (newImage == null)
                    return new OperationDto<ResponseDto>(OperationCodeDto.DoesNotExist, "La nueva imagen no existe en el sistema.");

                oldImageId = product.MediaFileId;
                product.MediaFileId = model.ImageId.Value;
            }

            return await _productRepository.ExecuteInTransactionWithSaveAsync(async () =>
            {
                // Guardar cambios del banner
                _productRepository.Update(product);

                // Eliminar imagen antigua
                if (oldImageId.HasValue)
                {
                    var oldImage = await _mediaFileRepository.Get(oldImageId.Value);
                    if (oldImage != null)
                        _mediaFileRepository.Delete(oldImage);
                }

                return new OperationDto<ResponseDto>(new ResponseDto() { Suceso = true, Mensaje = "Producto actualizado Correctamente" });
            });
        }
        public async Task<OperationDto<ResponseDto>> DeleteProduct(string userLoggedId, Guid productId)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<ResponseDto>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            // Buscar product
            var product = await _productRepository.GetProductDetail(productId);
            if (product == null)
                return new OperationDto<ResponseDto>(OperationCodeDto.DoesNotExist, "El producto no existe.");

            // Transacción
            return await _productRepository.ExecuteInTransactionWithSaveAsync(async () =>
            {
                // Eliminar banner
                _productRepository.Delete(product);

                // Eliminar mediaFile asociado
                var mediaFile = await _mediaFileRepository.Get(product.MediaFileId);
                if (mediaFile != null)
                    _mediaFileRepository.Delete(mediaFile);

                return new OperationDto<ResponseDto>(
                    new ResponseDto()
                    {
                        Suceso = true,
                        Mensaje = "Producto eliminado correctamente"
                    });
            });
        }
    }
}
