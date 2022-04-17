using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SeniaACSAPI.Models;

namespace SeniaACSAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Command> Commands { get; set; }
        public DbSet<Config> Configs { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceTypes> DeviceTypes { get; set; }
    }
}