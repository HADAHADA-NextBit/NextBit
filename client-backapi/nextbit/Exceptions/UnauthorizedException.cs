using System.Net;

namespace nextbit.Exceptions
{
    public class UnauthorizedException : HttpException
    {
        public UnauthorizedException(string? message = null, int? errorCode = null) : base(HttpStatusCode.BadRequest, message, errorCode)
        {
        }
    }
}
