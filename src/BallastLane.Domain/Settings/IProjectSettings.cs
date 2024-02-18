namespace BallastLane.Domain.Settings
{
    public interface IProjectSettings
    {
        public string DatabaseConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string PasswordSalt { get; set; }
    }
}
