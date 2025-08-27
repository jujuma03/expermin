using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Dtos.Portal.Testimony
{
    public class TestimonyUpdateDto
    {
        [JsonPropertyName("clientName")]
        public string ClientName { get; set; }
        [JsonPropertyName("rating")]
        public int? Rating { get; set; }
        [JsonPropertyName("comment")]
        public string Comment { get; set; }
        [JsonPropertyName("status")]
        public int? Status { get; set; }
        [JsonPropertyName("order")]
        public int? Order { get; set; }
        [JsonPropertyName("imageId")]
        public Guid? ImageId { get; set; }
    }
}
