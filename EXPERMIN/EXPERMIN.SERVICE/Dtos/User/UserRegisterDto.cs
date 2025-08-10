using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Dtos.User
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "El usuario es requerido")]
        [JsonPropertyName("user_name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        [JsonPropertyName("nombre")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El apellido es requerido")]
        [JsonPropertyName("apellido")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "El email es requerido")]
        [JsonPropertyName("correo")]
        [EmailAddress(ErrorMessage = "El campo debe ser un correo electrónico válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La contraseña es requerido")]
        [JsonPropertyName("contraseña")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 30 caracteres")]
        public string Password { get; set; }
        [JsonPropertyName("rol")]
        public int? Role { get; set; }
    }
}
