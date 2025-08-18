using EXPERMIN.CORE.Helpers;
using EXPERMIN.SERVICE.Dtos.Portal.Banner;
using EXPERMIN.SERVICE.Dtos.Portal.MediFile;
using EXPERMIN.SERVICE.Dtos.Portal.Product;
using EXPERMIN.WEB.Models.Portal.Banner;
using EXPERMIN.WEB.Models.Portal.MediaFile;
using EXPERMIN.WEB.Models.Portal.Product;
using EXPERMIN.WEB.Services.Portal.Portal.Interfaces;
using System.Net.Http;
using System.Text.Json;

namespace EXPERMIN.WEB.Services.Portal.Portal.Implementations
{
    public class PortalService : IPortalService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public PortalService(IHttpClientFactory httpClientFactory, ILogger<PortalService> logger)
        {
            _httpClient = httpClientFactory.CreateClient("ExperminApi");
            _logger = logger;
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
                    },
                    SequenceOrderId = b.SequenceOrderId.HasValue ? b.SequenceOrderId.Value : 0,
                    RouteType = b.RouteType == ConstantHelpers.BANNER.BUTTON.TYPE.INTERNAL ? true : false,
                    NameDirection = b.NameDirection,
                    StatusDirection = b.StatusDirection == ConstantHelpers.BANNER.BUTTON.SHOW ? true : false,
                    UrlDirection = b.UrlDirection
                })
                .OrderBy(p => p.SequenceOrderId)
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
                    },
                    Order = b.Order
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
    }
}