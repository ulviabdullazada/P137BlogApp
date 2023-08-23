namespace BlogApp.Business.Exceptions.Common
{
    public class NegativeIdException : Exception
    {
        public NegativeIdException() : base("Id 0-dan kiçik və ya bərabər ola bilməz") { }
        public NegativeIdException(string? message) : base(message)
        {
        }
    }
}
