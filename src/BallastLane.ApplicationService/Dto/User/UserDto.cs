namespace BallastLane.ApplicationService.Dto.User
{
    public class UserDto
    {
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}
