namespace BlogApp.Business.Exceptions.UserExceptios;

public class UserExistException : Exception
{
    public UserExistException():base("Username or email already exist")
    {
    }

    public UserExistException(string? message) : base(message)
    {
    }
}
