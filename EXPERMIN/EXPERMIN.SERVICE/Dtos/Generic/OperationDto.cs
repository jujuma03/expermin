using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.SERVICE.Dtos.Generic
{
    public class OperationDto<T>
    {
        public bool Completed { get; }
        public OperationCodeDto Code { get; }
        public T Result { get; }
        public List<string> Message { get; }

        public OperationDto(OperationCodeDto code, string message)
        {
            Code = (code == OperationCodeDto.EmptyResult) ? OperationCodeDto.Success : code;
            Message = new List<string>() { message };
            Completed = (code == OperationCodeDto.EmptyResult) ? true : false;
        }

        public OperationDto(OperationCodeDto code, List<string> messages)
        {
            Code = code;
            Message = messages;
            Completed = false;
        }
        public OperationDto(T result)
        {
            Completed = !EqualityComparer<T>.Default.Equals(result, default);
            Message = !Completed ? new List<string>() { "Without result" } : null;
            Code = !Completed ? OperationCodeDto.EmptyResult : OperationCodeDto.Success;
            Result = result;
        }
        public OperationDto(T result, string message)
        {
            Completed = !EqualityComparer<T>.Default.Equals(result, default);
            Message = new List<string>() { message };
            Code = !Completed ? OperationCodeDto.EmptyResult : OperationCodeDto.Success;
            Result = result;
        }
        public OperationDto(T result, OperationCodeDto code)
        {
            Completed = !EqualityComparer<T>.Default.Equals(result, default);
            Message = !Completed ? new List<string>() { "Without result" } : null;
            Code = !Completed ? OperationCodeDto.EmptyResult : code;
            Result = result;
        }
    }
}
