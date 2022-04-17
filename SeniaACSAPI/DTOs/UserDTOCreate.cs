namespace SeniaACSAPI.DTOs
{
    public class UserDTOCreate
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
