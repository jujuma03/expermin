using EXPERMIN.SERVICE.Dtos.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Services.User.Interfaces
{
    public interface IUserValidationService
    {
        Task<OperationDto<T>> ValidateUserAsync<T>(string userLoggedId, params string[] allowedRoles);
    }
}
