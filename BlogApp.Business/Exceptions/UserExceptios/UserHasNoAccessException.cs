namespace BlogApp.Business.Exceptions.UserExceptios;

public class UserHasNoAccessException : Exception
{
    public UserHasNoAccessException() : base("User has not access for that command") { }

    public UserHasNoAccessException(string? message) : base(message)
    {
    }
}
