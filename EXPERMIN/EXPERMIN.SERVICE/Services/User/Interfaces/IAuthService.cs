using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Services.User.Interfaces
{
    public interface IAuthService
    {
        Task<OperationDto<UserDto>> RegisterAccountAdmin(UserRegisterDto model);
        Task<OperationDto<UserLoginResponseDto>> Login(UserLoginDto model);
        Task<OperationDto<ResponseDto>> Logout(string token, string userLoggedId);
    }
}
