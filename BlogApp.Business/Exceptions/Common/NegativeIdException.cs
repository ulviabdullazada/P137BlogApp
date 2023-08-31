using Microsoft.AspNetCore.Http;

namespace BlogApp.Business.Exceptions.Common
{
    public class NegativeIdException : Exception, IBaseException
    {
        public int StatusCode => StatusCodes.Status400BadRequest;
        public string ErrorMessage { get; }
        public NegativeIdException()
        {
            ErrorMessage = "Id 0-dan kiçik və ya bərabər ola bilməz";
        }
        public NegativeIdException(string? message) : base(message)
        {
            ErrorMessage = message;
        }
    }
}
