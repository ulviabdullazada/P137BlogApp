using BlogApp.Business.Exceptions.Common;
using Microsoft.AspNetCore.Http;

namespace BlogApp.Business.Exceptions.Role;

public class RoleCreateFailedException : Exception, IBaseException
{
    public int StatusCode => StatusCodes.Status400BadRequest;
    public string ErrorMessage { get; }
    public RoleCreateFailedException()
    {
        ErrorMessage = "Something went wrong";
    }

    public RoleCreateFailedException(string? message) : base(message)
    {
        ErrorMessage = message;
    }


}
