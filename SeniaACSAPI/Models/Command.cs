namespace SeniaACSAPI.Models
{
    public class Command
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string ExecCommand { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public DateTime ExecutedAt { get; set; }
        public User ExecutedBy { get; set; }

    }
}
