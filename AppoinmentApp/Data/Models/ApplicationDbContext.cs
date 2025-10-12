using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace AppoinmentApp.Data.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Appoinment> Appoinments { get; set; }
    }
}
