using EXPERMIN.SERVICE.Dtos.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Dtos.User
{
    public class UserDetailDto
    {
        public string Id { get; set; }
        [JsonPropertyName("nombre")]
        public string Name { get; set; }
        [JsonPropertyName("apellido")]
        public string LastName { get; set; }
        [JsonPropertyName("correo")]
        public string Email { get; set; }
        [JsonPropertyName("roles")]
        public List<RoleDto> Roles { get; set; }

    }
}
