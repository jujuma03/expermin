using EXPERMIN.SERVICE.Dtos.Portal.MediFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Dtos.Portal.Collaborator
{
    public class CollaboratorDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("statusId")]
        public int StatusId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("orderId")]
        public byte? OrderId { get; set; }

        [JsonPropertyName("order")]
        public string Order { get; set; }

        [JsonPropertyName("publicationDate")]
        public string PublicationDate { get; set; }

        [JsonPropertyName("image")]
        public MediaFileDto Image { get; set; }
    }
}
