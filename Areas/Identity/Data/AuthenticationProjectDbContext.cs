using AuthenticationProject.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationProject.Data
{
    public class AuthenticationProjectDbContext : IdentityDbContext<AuthenticationProjectUser>
    {
        public AuthenticationProjectDbContext(DbContextOptions<AuthenticationProjectDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Task entity
            modelBuilder.Entity<AuthenticationProject.Models.Task>()
                .HasKey(t => t.Id);
        }

        // DbSet for Task
        public DbSet<AuthenticationProject.Models.Task> Tasks { get; set; }
    }
}
