using EXPERMIN.CORE.Helpers;
using EXPERMIN.ENTITIES.Models;
using EXPERMIN.REPOSITORY.Repositories.User.Interfaces;
using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.User;
using EXPERMIN.SERVICE.Security.Interfaces;
using EXPERMIN.SERVICE.Services.User.Interfaces;
using Microsoft.AspNetCore.Identity;
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
        private readonly IJwtService _jwtService;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthService(IUserRepository userRepository, IPasswordService passwordService, IJwtService jwtService, 
            RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _jwtService = jwtService;
            _roleManager = roleManager;
            _userManager = userManager;
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
            if (!await _roleManager.RoleExistsAsync(ConstantHelpers.ROLES.ADMIN))
            {
                var roleResult = await _roleManager.CreateAsync(new ApplicationRole
                {
                    Name = ConstantHelpers.ROLES.ADMIN
                });

                if (!roleResult.Succeeded)
                    return new OperationDto<UserDto>(OperationCodeDto.OperationError, "No se pudo crear el rol Admin");
            }

            //Verificar si no existe usuario con ese username
            if (await _userRepository.AnyByUsername(model.UserName))
                return new OperationDto<UserDto>(OperationCodeDto.AlreadyExists, "Ya existe usuario con ese username");

            //Verificar si no existe usuario con ese email
            if (await _userRepository.AnyByEmail(model.Email))
                return new OperationDto<UserDto>(OperationCodeDto.AlreadyExists, "Ya existe usuario con ese correo");

            //Crear usuario
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Name = model.Name,
                LastName = model.LastName,
                Email = model.Email
            };

            return await _userRepository.ExecuteInTransactionAsync(async () =>
            {
                var createUserResult = await _userManager.CreateAsync(user, model.Password);
                if (!createUserResult.Succeeded)
                    return new OperationDto<UserDto>(
                        OperationCodeDto.OperationError,
                        string.Join(", ", createUserResult.Errors.Select(e => e.Description))
                    );

                var roleResult = await _userManager.AddToRoleAsync(user, ConstantHelpers.ROLES.ADMIN);
                if (!roleResult.Succeeded)
                    return new OperationDto<UserDto>(
                        OperationCodeDto.OperationError,
                        string.Join(", ", roleResult.Errors.Select(e => e.Description))
                    );

                return new OperationDto<UserDto>(
                    new UserDto { Id = user.Id, UserName = user.UserName },
                    "Se creó correctamente el usuario."
                );
            });

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
