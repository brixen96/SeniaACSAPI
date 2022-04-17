using System.ComponentModel.DataAnnotations.Schema;

namespace SeniaACSAPI.Models
{
    public class Device
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string Name { get; set; }
        public string? SerialNumber { get; set; }
        public string? MAC { get; set; }
        public string? Model { get; set; }
        public string? IpAddress { get; set; }
        public string? SSHUsername { get; set; }
        public string? SSHPassword { get; set; }
    }
}
