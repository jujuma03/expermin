using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Dtos.User
{
    public class UserUpdateDto
    {
        [JsonPropertyName("user_name")]
        public string UserName { get; set; }
        [JsonPropertyName("nombre")]
        public string Name { get; set; }
        [JsonPropertyName("apellido")]
        public string LastName { get; set; }
        [JsonPropertyName("correo")]
        [EmailAddress(ErrorMessage = "El campo debe ser un correo electrónico válido")]
        public string Email { get; set; }
        [JsonPropertyName("rol")]
        public int? Role { get; set; }
    }
}
