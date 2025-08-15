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
    public class BannerRegisterDto
    {
        [Required(ErrorMessage = "El {0} es requerido")]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [JsonPropertyName("headline")]
        public string Headline { get; set; }
        [JsonPropertyName("Description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [JsonPropertyName("publicationDate")]
        public string PublicationDate { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [JsonPropertyName("routeType")]
        public string RouteType { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [JsonPropertyName("urlImage")]
        public string UrlImage { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [JsonPropertyName("sequenceOrder")]
        public string SequenceOrder { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [JsonPropertyName("urlDirection")]
        public string UrlDirection { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [JsonPropertyName("statusDirection")]
        public string StatusDirection { get; set; }
        [JsonPropertyName("nameDirection")]
        public string NameDirection { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        public IFormFile Image { get; set; }
    }
}
