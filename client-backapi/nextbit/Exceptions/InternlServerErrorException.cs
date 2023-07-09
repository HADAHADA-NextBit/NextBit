using System.Net;

namespace nextbit.Exceptions
{
    public class InternalServerErrorException : HttpException
    {
        public InternalServerErrorException(string? message = null, int? errorCode = null) : base(HttpStatusCode.BadRequest, message, errorCode)
        {
        }
    }
}
