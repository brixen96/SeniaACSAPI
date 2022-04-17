using System.ComponentModel.DataAnnotations.Schema;

namespace SeniaACSAPI.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public byte[]? PasswordHash { get; set;}
        public byte[]? PasswordSalt { get; set; }

        public string Role { get; set; } = "User";
        public bool? Deactivated { get; set; } = false;
    }
}
