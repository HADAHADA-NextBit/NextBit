using System.Net;

namespace nextbit.Exceptions
{
    public class ForbiddenException : HttpException
    {
        public ForbiddenException(string? message = null, int? errorCode = null) : base(HttpStatusCode.BadRequest, message, errorCode)
        {
        }
    }
}
