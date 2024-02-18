using BallastLane.Domain.Settings;

namespace Domain.Settings
{
    public class ProjectSettings : IProjectSettings
    {
        public string DatabaseConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string PasswordSalt { get; set; }
    }
}
