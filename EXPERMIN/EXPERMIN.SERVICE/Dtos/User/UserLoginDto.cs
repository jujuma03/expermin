using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Dtos.User
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "El usuario es requerido")]
        [JsonPropertyName("user_name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "La contraseña es requerido")]
        [JsonPropertyName("contraseña")]
        public string Password { get; set; }
    }
}
