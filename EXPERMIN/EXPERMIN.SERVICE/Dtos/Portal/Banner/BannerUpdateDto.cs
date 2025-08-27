using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Dtos.Portal.Banner
{
    public class BannerUpdateDto
    {
        [JsonPropertyName("headline")]
        public string Headline { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("status")]
        public int? Status { get; set; }
        [JsonPropertyName("routeType")]
        public int? RouteType { get; set; }
        [JsonPropertyName("order")]
        public int? Order { get; set; }
        [JsonPropertyName("urlDirection")]
        public string UrlDirection { get; set; }
        [JsonPropertyName("statusDirection")]
        public int? StatusDirection { get; set; }
        [JsonPropertyName("nameDirection")]
        public string NameDirection { get; set; }
        [JsonPropertyName("imageId")]
        public Guid? ImageId { get; set; }
    }
}
