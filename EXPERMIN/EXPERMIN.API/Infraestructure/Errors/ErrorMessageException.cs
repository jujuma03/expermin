using System.Net;

namespace EXPERMIN.API.Infraestructure.Errors
{
    public class ErrorMessageException : Exception
    {
        public HttpStatusCode HttpCode { get; private set; }
        public int CodigoError { get; private set; }

        public List<string> Errores { get; private set; }

        public ErrorMessageException(HttpStatusCode httpCode, int codigoError, List<string> errores)
        {
            HttpCode = httpCode;
            CodigoError = codigoError;
            Errores = errores;
        }
    }
}
