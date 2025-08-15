using EXPERMIN.SERVICE.Dtos.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EXPERMIN.API.Controllers
{

    public class BaseController : ControllerBase
    {
        protected ActionResult GenerateBadRequestError(int errorCode, List<string> errors)
        {
            return BadRequest(new { errorCode, errors });
        }
        protected ActionResult GenerateUnauthorizedError(List<string> errors)
        {
            return Unauthorized(new { errorCode = (int)HttpStatusCode.Unauthorized, errors });
        }
        protected ActionResult GenerateAlreadyExistsError(List<string> errors)
        {
            return Conflict(new { errorCode = (int)HttpStatusCode.Conflict, errors });
        }
        protected ActionResult GenerateNotFoundError(List<string> errors)
        {
            return NotFound(new { errorCode = (int)HttpStatusCode.NotFound, errors });
        }
        protected ActionResult GenerateErrorOperation<T>(OperationDto<T> operationDto)
        {
            return BadRequest(new { errorCode = (int)operationDto.Code, message = operationDto.Message });
        }
        protected ActionResult GenerateErrorIfNoJsonBody(System.Object objectDto)
        {
            if (objectDto == null)
            {
                return GenerateBadRequestError((int)HttpStatusCode.BadRequest, new List<string> { "Parámetros de ingreso inválidos" });
            }
            return Ok();
        }

        protected ActionResult<T> GetResultOrGenerateOperationError<T>(OperationDto<T> operation, string message = null)
        {
            if (!operation.Completed)
            {
                switch (operation.Code)
                {
                    case OperationCodeDto.DoesNotExist:
                        return GenerateNotFoundError(operation.Message);
                    case OperationCodeDto.InvalidAccess:
                        return GenerateBadRequestError((int)HttpStatusCode.BadRequest, operation.Message);
                    case OperationCodeDto.Unauthorized:
                        return GenerateUnauthorizedError(operation.Message);
                    case OperationCodeDto.AlreadyExists:
                        return GenerateAlreadyExistsError(operation.Message);
                    default:
                        return GenerateErrorOperation(operation);
                }
            }

            // Si la lista de mensajes está vacía o solo contiene strings vacíos, devolver solo el objeto sin array
            if (operation.Message == null || !operation.Message.Any(m => !string.IsNullOrWhiteSpace(m)))
                return Ok(operation.Result);

            // Si operation.Result es null, devolver solo el mensaje sin array
            if (operation.Result == null)
                return Ok(new { mensaje = message ?? string.Join(", ", operation.Message) });

            // Si hay mensajes, devolver como array con mensaje + resultado
            return Ok(new object[]
            {
                new { mensaje = message ?? string.Join(", ", operation.Message) },
                operation.Result == null ? "" : operation.Result
            });
        }
    }
}
