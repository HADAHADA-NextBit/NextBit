using System.Net;

namespace nextbit.Exceptions
{
    public class ConflictException : HttpException
    {
        public ConflictException(string? message = null, int? errorCode = null) : base(HttpStatusCode.BadRequest, message, errorCode)
        {
        }
    }
}
