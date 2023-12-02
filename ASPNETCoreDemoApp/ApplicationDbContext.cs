using ASPNETCoreDemoApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreDemoApp
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Person> People { get; set; }
    }
}
