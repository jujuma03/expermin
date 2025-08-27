using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Dtos.Portal.Testimony
{
    public class TestimonyRegisterDto
    {
        [Required(ErrorMessage = "El NOMBRE DEL CLIENTE es requerido")]
        [JsonPropertyName("clientName")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "El PUNTAJE DEL CLIENTE es requerido")]
        [JsonPropertyName("rating")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "El COMENTARIO DEL CLIENTE es requerido")]
        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        [Required(ErrorMessage = "El ESTADO(1 = MOSTRAR, 2 = NO MOSTRAR) es requerido")]
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("imageId")]
        public Guid ImageId { get; set; }
    }
}
