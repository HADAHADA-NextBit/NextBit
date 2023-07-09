using System.Net;
using nextbit.Exceptions;

namespace nextbit.Exceptions
{
    public class BadRequestException : HttpException
    {
        public BadRequestException(string? message = null, int? errorCode = null) : base(HttpStatusCode.BadRequest, message, errorCode)
        {
        }
    }
}
