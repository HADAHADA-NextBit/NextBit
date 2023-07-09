using System.Net;

namespace nextbit.Exceptions
{
    public class NotFoundException : HttpException
    {
        public NotFoundException(string? message = null, int? errorCode = null) : base(HttpStatusCode.BadRequest, message, errorCode)
        {
        }
    }
}
