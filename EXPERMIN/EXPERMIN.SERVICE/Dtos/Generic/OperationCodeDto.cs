using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Dtos.Generic
{

    public enum OperationCodeDto
    {
        Success,
        EmptyResult,
        IncorrectUser,
        DisabledUser,
        OperationNotAvailable,
        InvalidAccess,
        DoesNotExist,
        ServerError,
        Invalid,
        InvalidParameters,
        InvalidInput,
        Unauthorized,
        AlreadyExists,
        Forbidden,
        OperationError
    }
}
