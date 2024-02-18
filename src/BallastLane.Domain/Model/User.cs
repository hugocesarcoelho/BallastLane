namespace BallastLane.Domain.Model
{
    public class User: ModelBase
    {
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
