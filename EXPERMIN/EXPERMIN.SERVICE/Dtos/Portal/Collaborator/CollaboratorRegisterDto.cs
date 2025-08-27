using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Dtos.Portal.Collaborator
{
    public class CollaboratorRegisterDto
    {
        [Required(ErrorMessage = "El NOMBRE DEL COLABORADOR es requerido")]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "El ESTADO(1 = MOSTRAR, 2 = NO MOSTRAR) es requerido")]
        [JsonPropertyName("status")]
        public int Status { get; set; }

        [JsonPropertyName("order")]
        public int Order { get; set; }

        [JsonPropertyName("imageId")]
        public Guid ImageId { get; set; }
    }
}
