using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Dtos.Portal.Banner
{
    public class BannerDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("headline")]
        public string Headline { get; set; }
        [JsonPropertyName("urlImage")]
        public string UrlImage { get; set; }
        [JsonPropertyName("publicationDate")]
        public string PublicationDate { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("sequenceOrder")]
        public string SequenceOrder { get; set; }
        [JsonPropertyName("sequenceOrderId")]
        public byte SequenceOrderId { get; set; }
    }
}
