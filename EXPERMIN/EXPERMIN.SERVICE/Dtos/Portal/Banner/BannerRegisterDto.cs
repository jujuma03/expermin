using EXPERMIN.SERVICE.Dtos.Portal.MediFile;
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
        [Required(ErrorMessage = "El TITULAR es requerido")]
        [JsonPropertyName("headline")]
        public string Headline { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El ESTADO(1 = MOSTRAR, 2 = NO MOSTRAR) es requerido")]
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [Required(ErrorMessage = "El TIPO DE RUTA DEL BOTÓN(1 = INTERNO, 2 = EXTERNO) es requerido")]
        [JsonPropertyName("routeType")]
        public int RouteType { get; set; }

        [Required(ErrorMessage = "El ORDEN DEL BANNER es requerido")]
        [JsonPropertyName("sequenceOrder")]
        public int SequenceOrder { get; set; }

        [JsonPropertyName("urlDirection")]
        public string UrlDirection { get; set; }

        [Required(ErrorMessage = "El MOSTRAR BOTÓN(1 = SÍ, 2 = NO) es requerido")]
        [JsonPropertyName("statusDirection")]
        public int StatusDirection { get; set; }

        [JsonPropertyName("nameDirection")]
        public string NameDirection { get; set; }

        [JsonPropertyName("imageId")]
        public Guid ImageId { get; set; }
    }
}
