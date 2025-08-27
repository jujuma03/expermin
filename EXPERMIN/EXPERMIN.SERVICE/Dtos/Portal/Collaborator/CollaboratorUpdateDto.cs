using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Dtos.Portal.Collaborator
{
    public class CollaboratorUpdateDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        public int? Status { get; set; }
        [JsonPropertyName("order")]
        public int? Order { get; set; }
        [JsonPropertyName("imageId")]
        public Guid? ImageId { get; set; }
    }
}
