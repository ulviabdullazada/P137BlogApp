namespace BlogApp.Business.Exceptions.UserExceptios;

public class RegisterFailedException : Exception
{
    public RegisterFailedException() : base("Register failed for some reasons")
    {
    }

    public RegisterFailedException(string? message) : base(message)
    {
    }
}
