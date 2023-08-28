namespace BlogApp.Business.Dtos.UserDtos
{
    public record AuthorDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string? ImageUrl { get; set; }
    }
}
