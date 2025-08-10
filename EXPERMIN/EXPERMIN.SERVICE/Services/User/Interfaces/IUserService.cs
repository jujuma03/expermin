using EXPERMIN.SERVICE.Dtos.Generic;
using EXPERMIN.SERVICE.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Services.User.Interfaces
{
    public interface IUserService
    {
        string GetUserId();
        Task<bool> UserExists(string userId);
        Task<OperationDto<List<UserDetailDto>>> GetAllUser(string userLoggedId);
        Task<OperationDto<UserDetailDto>> GetUserById(string userLoggedId, string id);
        Task<OperationDto<UserDto>> InsertUser(string userLoggedId, UserRegisterDto model);
        Task<OperationDto<UserDetailDto>> UpdateUser(string userLoggedId, string id, UserUpdateDto model);
        Task<OperationDto<ResponseDto>> DeleteUser(string id, string userLoggedId);
        Task<string> GenerateUniqueUserName(string baseUserName);
        Task<string> GeneratePassword();
    }
}
