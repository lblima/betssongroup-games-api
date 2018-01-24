namespace OnlineCassino.WebApi.DTOs
{
    public class GameSessionDto
    {
        public int Id { get; set; }
        public GameDto Game { get; set; }
        public UserDto User { get; set; }
        public bool IsInProgress { get; set; }
    }
}