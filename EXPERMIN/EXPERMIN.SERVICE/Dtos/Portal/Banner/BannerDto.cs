using EXPERMIN.SERVICE.Dtos.Portal.MediFile;
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

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("publicationDate")]
        public string PublicationDate { get; set; }

        [JsonPropertyName("statusId")]
        public byte StatusId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("routeType")]
        public int RouteType { get; set; }

        [JsonPropertyName("sequenceOrderId")]
        public byte? SequenceOrderId { get; set; }

        [JsonPropertyName("sequenceOrder")]
        public string SequenceOrder { get; set; }

        [JsonPropertyName("urlDirection")]
        public string UrlDirection { get; set; }

        [JsonPropertyName("statusDirection")]
        public int StatusDirection { get; set; }

        [JsonPropertyName("nameDirection")]
        public string NameDirection { get; set; }


        [JsonPropertyName("image")]
        public MediaFileDto Image { get; set; }
    }
}
