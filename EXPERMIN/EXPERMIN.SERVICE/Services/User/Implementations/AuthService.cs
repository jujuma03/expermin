using EXPERMIN.CORE.Helpers;
using EXPERMIN.ENTITIES.Models;
using EXPERMIN.REPOSITORY.Repositories.User.Interfaces;
using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.User;
using EXPERMIN.SERVICE.Security.Interfaces;
using EXPERMIN.SERVICE.Services.User.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Services.User.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IRoleRepository _roleRepository;
        private readonly IJwtService _jwtService;
        public AuthService(IUserRepository userRepository, IPasswordService passwordService, IJwtService jwtService, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _jwtService = jwtService;
            _roleRepository = roleRepository;
        }
        public async Task<OperationDto<UserLoginResponseDto>> Login(UserLoginDto model)
        {
            var user = await _userRepository.FindByUserNameAsync(model.UserName);
            if (user == null || !_passwordService.VerifyPassword(model.Password, user.PasswordHash))
                return new OperationDto<UserLoginResponseDto>(OperationCodeDto.Unauthorized, "Credenciales incorrectas");

            var token = _jwtService.GenerateToken(user);

            var response = new UserLoginResponseDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Token = token
            };

            return new OperationDto<UserLoginResponseDto>(response);
        }

        public async Task<OperationDto<UserDto>> RegisterAccountAdmin(UserRegisterDto model)
        {
            //Verificar si el rol "Admin" ya existe
            var adminRole = await _roleRepository.GetRoleByName(ConstantHelpers.ROLES.ADMIN);
            if (adminRole == null)
            {
                adminRole = new ApplicationRole { Name = ConstantHelpers.ROLES.ADMIN };
                await _roleRepository.AddRoleAsync(adminRole);
            }

            //Verificar si no existe usuario con ese username
            if (await _userRepository.AnyByUsername(model.UserName))
                return new OperationDto<UserDto>(OperationCodeDto.AlreadyExists, "Ya existe usuario con ese username");

            //Verificar si no existe usuario con ese email
            if (await _userRepository.AnyByEmail(model.Email))
                return new OperationDto<UserDto>(OperationCodeDto.AlreadyExists, "Ya existe usuario con ese correo");

            var passwordHash = _passwordService.HashPassword(model.Password);


            //Crear usuario
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Name = model.Name,
                LastName = model.LastName,
                Email = model.Email,
                PasswordHash = passwordHash
            };
            await _userRepository.Add(user);

            //Agregar Rol Admin a Usuario
            await _userRepository.AddToRoleAsync(user, adminRole.Name);

            var result = await _userRepository.CreateUserAsync(user, model.Password);
            return new OperationDto<UserDto>(new UserDto { Id = user.Id, UserName = user.UserName }, "Registro exitoso.");
        }
        public async Task<OperationDto<ResponseDto>> Logout(string token, string userLoggedId)
        {

            var result = await _jwtService.RevokedToken(token);
            if (!result)
                return new OperationDto<ResponseDto>(OperationCodeDto.Invalid, "Token inválido o ya revocado.");

            return new OperationDto<ResponseDto>(new ResponseDto() { Suceso = true, Mensaje = "Logout exitoso." });
        }
    }
}
