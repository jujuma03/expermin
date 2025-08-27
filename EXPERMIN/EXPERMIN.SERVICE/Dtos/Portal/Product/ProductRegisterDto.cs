using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Dtos.Portal.Product
{
    public class ProductRegisterDto
    {
        [Required(ErrorMessage = "El TITULO es requerido")]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("shortDescription")]
        public string ShortDescription { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El ESTADO(1 = MOSTRAR, 2 = NO MOSTRAR) es requerido")]
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("imageId")]
        public Guid ImageId { get; set; }
    }
}
