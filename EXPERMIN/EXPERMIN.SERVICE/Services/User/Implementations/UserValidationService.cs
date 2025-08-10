using EXPERMIN.REPOSITORY.Repositories.User.Interfaces;
using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Services.User.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Services.User.Implementations
{

    public class UserValidationService : IUserValidationService
    {
        private readonly IUserRepository _userRepository;

        public UserValidationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<OperationDto<T>> ValidateUserAsync<T>(string userLoggedId, params string[] allowedRoles)
        {
            // Validar si el ID del usuario es válido
            if (string.IsNullOrWhiteSpace(userLoggedId))
                return new OperationDto<T>(OperationCodeDto.Unauthorized, "El ID del usuario logeado es inválido.");

            // Verificar si el usuario existe
            if (!await _userRepository.ExistsAsync(userLoggedId))
                return new OperationDto<T>(OperationCodeDto.DoesNotExist, "No te encuentras registrado en la base de datos.");

            // Obtener roles del usuario
            var roles = await _userRepository.GetRolesAsync(userLoggedId);

            // Verificar si tiene alguno de los roles permitidos
            if (!roles.Any(r => allowedRoles.Contains(r.Name, StringComparer.OrdinalIgnoreCase)))
                return new OperationDto<T>(OperationCodeDto.Unauthorized, "No tienes permiso para este recurso.");

            return null;
        }
    }
}
