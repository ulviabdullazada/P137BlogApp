namespace BlogApp.Business.Exceptions.UserExceptios
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base("User not found")
        {
        }

        public UserNotFoundException(string? message) : base(message)
        {
        }
    }
}
