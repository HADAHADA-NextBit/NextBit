using System.Net;

namespace nextbit.Exceptions
{
    public abstract class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public int? ErrorCode { get; }

        public HttpException(HttpStatusCode statusCode, string? message, int? errorCode) : base(message)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }
    }
}
