namespace BlogApp.Business.Dtos.UserDtos;

public record TokenResponseDto
{
    public string Token { get; set; }
    public string Username { get; set; }
    public DateTime Expires { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpires { get; set; }
}
