namespace SeniaACSAPI.Models
{
    public class DeviceTypes
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string ProductClass  { get; set; }
        public string Version { get; set; }
        public string? Paired { get; set; }
        public string Description { get; set; }
    }
}
