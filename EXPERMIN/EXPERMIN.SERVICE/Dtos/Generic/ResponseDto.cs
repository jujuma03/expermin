using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Dtos.Generic
{
    public class ResponseDto
    {
        [JsonPropertyName("suceso")]
        public bool Suceso { get; set; }

        [JsonPropertyName("mensaje")]
        public string Mensaje { get; set; }

    }
}
