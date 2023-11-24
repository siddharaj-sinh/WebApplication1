using Microsoft.EntityFrameworkCore;
using WebApplication1.Modal;

namespace WebApplication1.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Dummy User" }
        );
            base.OnModelCreating(modelBuilder);

        }

        public DbSet<User> User { get; set; }
    }
}
