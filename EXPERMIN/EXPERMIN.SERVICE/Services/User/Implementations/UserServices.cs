using AutoMapper;
using EXPERMIN.CORE.Helpers;
using EXPERMIN.ENTITIES.Models;
using EXPERMIN.REPOSITORY.Repositories.User.Interfaces;
using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.User;
using EXPERMIN.SERVICE.Security.Interfaces;
using EXPERMIN.SERVICE.Services.User.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Services.User.Implementations
{

    public class UserServices : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserValidationService _userValidationService;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserServices(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository, IMapper mapper, IPasswordService passwordService, 
            IJwtService jwtService, IRoleRepository roleRepository, IUserValidationService userValidationService,
            RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordService = passwordService;
            _jwtService = jwtService;
            _roleRepository = roleRepository;
            _userValidationService = userValidationService;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public string GetUserId()
            => _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public async Task<bool> UserExists(string userId)
            => !string.IsNullOrWhiteSpace(userId) && await _userRepository.ExistsAsync(userId);
        public async Task<OperationDto<List<UserDetailDto>>> GetAllUser(string userLoggedId)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<List<UserDetailDto>>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            //Verificar si hay usuarios
            var users = await _userRepository.GetAllUserAsync();
            if (users == null || users.Count == 0)
                return new OperationDto<List<UserDetailDto>>(OperationCodeDto.EmptyResult, "No hay usuarios registradas.");

            var usersDto = _mapper.Map<List<UserDetailDto>>(users);

            return new OperationDto<List<UserDetailDto>>(usersDto);
        }
        public async Task<OperationDto<UserDetailDto>> GetUserById(string userLoggedId, string id)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<UserDetailDto>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            var user = await _userRepository.GetUserById(id);
            if (user == null)
                return new OperationDto<UserDetailDto>(OperationCodeDto.DoesNotExist, "El usuario no existe");

            var userDto = _mapper.Map<UserDetailDto>(user);
            return new OperationDto<UserDetailDto>(userDto);
        }
        public async Task<OperationDto<UserDto>> InsertUser(string userLoggedId, UserRegisterDto model)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<UserDto>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            //Verificar si no existe usuario con ese username
            if (await _userRepository.AnyByUsername(model.UserName))
                return new OperationDto<UserDto>(OperationCodeDto.AlreadyExists, "Ya existe usuario con ese username");

            //Verificar si no existe usuario con ese email
            if (await _userRepository.AnyByEmail(model.Email))
                return new OperationDto<UserDto>(OperationCodeDto.AlreadyExists, "Ya existe usuario con ese correo");

            if (!ConstantHelpers.ROLES.INDICES.TryGetValue(model.Role.Value, out string roleName))
                return new OperationDto<UserDto>(OperationCodeDto.Invalid, "El rol proporcionado no es válido.");

            //validar si existe el rol en la BD, sino crearlo
            var role = await _roleRepository.GetRoleByName(ConstantHelpers.ROLES.INDICES[model.Role.Value]);
            if (role == null)
            {
                role = new ApplicationRole { Name = ConstantHelpers.ROLES.INDICES[model.Role.Value] };
                await _roleRepository.CreateRoleAsync(role);
            }

            // Crear usuario (sin password hash manual, lo hace UserManager)
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Name = model.Name,
                LastName = model.LastName,
                Email = model.Email
            };

            // Ejecutar en transacción
            return await _userRepository.ExecuteInTransactionAsync(async () =>
            {
                var createUserResult = await _userManager.CreateAsync(user, model.Password);
                if (!createUserResult.Succeeded)
                {
                    return new OperationDto<UserDto>(
                        OperationCodeDto.OperationError,
                        string.Join(", ", createUserResult.Errors.Select(e => e.Description))
                    );
                }

                var roleResult = await _userManager.AddToRoleAsync(user, roleName);
                if (!roleResult.Succeeded)
                {
                    return new OperationDto<UserDto>(
                        OperationCodeDto.OperationError,
                        string.Join(", ", roleResult.Errors.Select(e => e.Description))
                    );
                }

                return new OperationDto<UserDto>(
                    new UserDto { Id = user.Id, UserName = user.UserName },
                    "Se creó correctamente el usuario."
                );
            });
        }
        public async Task<OperationDto<UserDetailDto>> UpdateUser(string userLoggedId, string id, UserUpdateDto model)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<UserDetailDto>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            // Obtener el usuario
            var user = await _userRepository.GetUserById(id);
            if (user == null)
                return new OperationDto<UserDetailDto>(OperationCodeDto.DoesNotExist, "No existe el usuario.");

            return await _userRepository.ExecuteInTransactionAsync(async () =>
            {
                // Si se pasó un rol nuevo
                if (model.Role.HasValue)
                {
                    if (!ConstantHelpers.ROLES.INDICES.TryGetValue(model.Role.Value, out string roleName))
                        return new OperationDto<UserDetailDto>(OperationCodeDto.Invalid, "El rol proporcionado no es válido.");

                    // Crear rol si no existe
                    if (!await _roleManager.RoleExistsAsync(roleName))
                    {
                        var roleResult = await _roleManager.CreateAsync(new ApplicationRole { Name = roleName });
                        if (!roleResult.Succeeded)
                        {
                            return new OperationDto<UserDetailDto>(
                                OperationCodeDto.OperationError,
                                $"No se pudo crear el rol {roleName}: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}"
                            );
                        }
                    }

                    // Obtener roles actuales del usuario
                    var currentRoles = await _userManager.GetRolesAsync(user);

                    // Remover todos los roles actuales
                    var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    if (!removeResult.Succeeded)
                    {
                        return new OperationDto<UserDetailDto>(
                            OperationCodeDto.OperationError,
                            string.Join(", ", removeResult.Errors.Select(e => e.Description))
                        );
                    }

                    // Asignar nuevo rol
                    var addRoleResult = await _userManager.AddToRoleAsync(user, roleName);
                    if (!addRoleResult.Succeeded)
                    {
                        return new OperationDto<UserDetailDto>(
                            OperationCodeDto.OperationError,
                            string.Join(", ", addRoleResult.Errors.Select(e => e.Description))
                        );
                    }
                }

                // Mapear cambios (Name, LastName, Email, etc.)
                _mapper.Map(model, user);

                // Actualizar en Identity
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    return new OperationDto<UserDetailDto>(
                        OperationCodeDto.OperationError,
                        string.Join(", ", updateResult.Errors.Select(e => e.Description))
                    );
                }

                var result = _mapper.Map<UserDetailDto>(user);

                return new OperationDto<UserDetailDto>(result, "Usuario actualizado correctamente");
            });
        }
        public async Task<OperationDto<ResponseDto>> DeleteUser(string id, string userLoggedId)
        {
            // Solo ADMIN permitido
            var validationResult = await _userValidationService.ValidateUserAsync<ResponseDto>(
                userLoggedId, ConstantHelpers.ROLES.ADMIN);
            if (validationResult != null)
                return validationResult;

            //Obtener el usuario
            var user = await _userRepository.GetUserById(id);
            if (user == null)
                return new OperationDto<ResponseDto>(OperationCodeDto.DoesNotExist, "No existe el usuario.");

            _userRepository.Delete(user);

            return new OperationDto<ResponseDto>
                     (new ResponseDto() { Suceso = true, Mensaje = "Usuario eliminado Correctamente" });
        }
        public async Task<string> GenerateUniqueUserName(string baseUserName)
        {
            var userName = baseUserName.ToLower();
            int suffix = 1;

            //Verificar si no existe usuario con ese username
            if (await _userRepository.AnyByUsername(baseUserName))
                userName = $"{baseUserName.ToLower()}{suffix}";
            return userName;
        }
        public async Task<string> GeneratePassword()
        {
            var passwordOptions = _userRepository.GetPasswordOptions();
            var random = new Random();
            var passwordChars = new List<char>();

            if (passwordOptions.RequireUppercase)
                passwordChars.Add((char)random.Next(65, 90)); // A-Z

            if (passwordOptions.RequireLowercase)
                passwordChars.Add((char)random.Next(97, 122)); // a-z

            if (passwordOptions.RequireDigit)
                passwordChars.Add((char)random.Next(48, 57)); // 0-9

            if (passwordOptions.RequireNonAlphanumeric)
            {
                var specialChars = "!@#$%^&*()-_=+<>?";
                passwordChars.Add(specialChars[random.Next(specialChars.Length)]);
            }

            // Completar con caracteres aleatorios hasta llegar a la longitud mínima
            while (passwordChars.Count < passwordOptions.RequiredLength)
            {
                passwordChars.Add((char)random.Next(33, 126)); // Caracteres imprimibles
            }

            // Mezclar los caracteres para mayor seguridad
            return new string(passwordChars.OrderBy(x => random.Next()).ToArray());
        }
    }
}
