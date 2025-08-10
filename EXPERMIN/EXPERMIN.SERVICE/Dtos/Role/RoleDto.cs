using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Dtos.Role
{
    public class RoleDto
    {
        public string Id { get; set; }

        [JsonPropertyName("rol")]
        public string Name { get; set; }
    }
}
