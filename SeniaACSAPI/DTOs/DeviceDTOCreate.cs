namespace SeniaACSAPI.DTOs
{
    public class DeviceDTOCreate
    {
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string? IpAddress { get; set; }
        public string? SSHUsername { get; set; }
        public string? SSHPassword { get; set; }
    }
}
