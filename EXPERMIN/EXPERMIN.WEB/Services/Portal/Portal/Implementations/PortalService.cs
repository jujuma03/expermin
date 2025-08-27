using EXPERMIN.CORE.Helpers;
using EXPERMIN.SERVICE.Dtos.Portal.Banner;
using EXPERMIN.SERVICE.Dtos.Portal.Collaborator;
using EXPERMIN.SERVICE.Dtos.Portal.MediFile;
using EXPERMIN.SERVICE.Dtos.Portal.Product;
using EXPERMIN.SERVICE.Dtos.Portal.Testimony;
using EXPERMIN.SERVICE.Storage.Model;
using EXPERMIN.WEB.Models.Portal.Banner;
using EXPERMIN.WEB.Models.Portal.Collaborator;
using EXPERMIN.WEB.Models.Portal.MediaFile;
using EXPERMIN.WEB.Models.Portal.Product;
using EXPERMIN.WEB.Models.Portal.Testimony;
using EXPERMIN.WEB.Services.Portal.Portal.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Text.Json;

namespace EXPERMIN.WEB.Services.Portal.Portal.Implementations
{
    public class PortalService : IPortalService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;
        private readonly StorageOptions _settings;

        public PortalService(IHttpClientFactory httpClientFactory, ILogger<PortalService> logger, IOptions<StorageOptions> settings)
        {
            _httpClient = httpClientFactory.CreateClient("ExperminApi");
            _logger = logger;
            _settings = settings.Value;
        }
        public async Task<List<BannerViewModel>> GetAllBannersActiveAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/v1/banner/get-all");

                if (!response.IsSuccessStatusCode) return new List<BannerViewModel>();

                var json = await response.Content.ReadAsStringAsync();
                var banners = JsonSerializer.Deserialize<List<BannerDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return banners?.Select(b => new BannerViewModel
                {
                    Id = b.Id,
                    Headline = b.Headline,
                    Description = b.Description,
                    Image = b.Image == null ? null : new MediaFileViewModel
                    {
                        Id = b.Image.Id,
                        FileName = b.Image.FileName,
                        Path = b.Image.Path,
                        Url = b.Image.Url,
                        FullUrl = $"{_settings.BaseUrl.TrimEnd('/')}/{b.Image.Url.TrimStart('/')}"
                    },
                    Order = b.OrderId.HasValue ? b.OrderId.Value : 0,
                    RouteType = b.RouteType == ConstantHelpers.BANNER.BUTTON.TYPE.INTERNAL ? true : false,
                    NameDirection = b.NameDirection,
                    StatusDirection = b.StatusDirection == ConstantHelpers.BANNER.BUTTON.SHOW ? true : false,
                    UrlDirection = b.UrlDirection
                })
                .OrderBy(p => p.Order)
                .ToList() ?? new List<BannerViewModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo productos activos"); // mejor log
                return new List<BannerViewModel>();
            }
        }
        public async Task<List<ProductViewModel>> GetAllProductsActiveAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/v1/product/get-all");

                if (!response.IsSuccessStatusCode) return new List<ProductViewModel>();

                var json = await response.Content.ReadAsStringAsync();
                var products = JsonSerializer.Deserialize<List<ProductDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return products?.Select(b => new ProductViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    
                    Description = b.Description,
                    Image = b.Image == null ? null : new MediaFileViewModel
                    {
                        Id = b.Image.Id,
                        FileName = b.Image.FileName,
                        Path = b.Image.Path,
                        Url = b.Image.Url,
                        FullUrl = $"{_settings.BaseUrl.TrimEnd('/')}/{b.Image.Url.TrimStart('/')}"
                    },
                    Order = b.OrderId.HasValue ? b.OrderId.Value : 0,
                })
                .OrderBy(p => p.Order)
                .ToList() ?? new List<ProductViewModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo productos activos"); // mejor log
                return new List<ProductViewModel>();
            }
        }
        public async Task<List<TestimonyViewModel>> GetAllTestimonysActiveAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/v1/testimony/get-all");

                if (!response.IsSuccessStatusCode) return new List<TestimonyViewModel>();

                var json = await response.Content.ReadAsStringAsync();
                var testimonys = JsonSerializer.Deserialize<List<TestimonyDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return testimonys?.Select(b => new TestimonyViewModel
                {
                    Id = b.Id,
                    ClientName = b.ClientName,
                    Rating = b.Rating,
                    Comment = b.Comment,
                    Image = b.Image == null ? null : new MediaFileViewModel
                    {
                        Id = b.Image.Id,
                        FileName = b.Image.FileName,
                        Path = b.Image.Path,
                        Url = b.Image.Url,
                        FullUrl = $"{_settings.BaseUrl.TrimEnd('/')}/{b.Image.Url.TrimStart('/')}"
                    },
                    Order = b.OrderId.HasValue ? b.OrderId.Value : 0,
                })
                .OrderBy(p => p.Order)
                .ToList() ?? new List<TestimonyViewModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo testimonios activos"); // mejor log
                return new List<TestimonyViewModel>();
            }
        }
        public async Task<List<CollaboratorViewModel>> GetAllCollaboratorsActiveAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/v1/collaborator/get-all");

                if (!response.IsSuccessStatusCode) return new List<CollaboratorViewModel>();

                var json = await response.Content.ReadAsStringAsync();
                var collaborators = JsonSerializer.Deserialize<List<CollaboratorDto>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return collaborators?.Select(b => new CollaboratorViewModel
                {
                    Id = b.Id,
                    Name = b.Name,
                    Image = b.Image == null ? null : new MediaFileViewModel
                    {
                        Id = b.Image.Id,
                        FileName = b.Image.FileName,
                        Path = b.Image.Path,
                        Url = b.Image.Url,
                        FullUrl = $"{_settings.BaseUrl.TrimEnd('/')}/{b.Image.Url.TrimStart('/')}"
                    },
                    Order = b.OrderId.HasValue ? b.OrderId.Value : 0,
                })
                .OrderBy(p => p.Order)
                .ToList() ?? new List<CollaboratorViewModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error obteniendo colaboradores activos"); // mejor log
                return new List<CollaboratorViewModel>();
            }
        }
    }
}