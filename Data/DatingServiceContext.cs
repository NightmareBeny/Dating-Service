using Microsoft.EntityFrameworkCore;
using DatingService.Models;

namespace DatingService.Data
{
    public class DatingServiceContext:DbContext
    {
        public DatingServiceContext(DbContextOptions<DatingServiceContext> options) : base(options)
        {
        }

        public DbSet<Client> Client { get; set; }
        public DbSet<Partner> Partner { get; set; }
        public DbSet<Couple> Couple { get; set; }
        public DbSet<Zodiac> Zodiac { get; set; }
        public DbSet<Employee> Employee { get; set; }
    }
}
