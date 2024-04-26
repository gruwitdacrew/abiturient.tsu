using Document_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Document_Service.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<EducationDocument> Educations { get; set; }
        public DbSet<PassportDocument> Passports { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EducationDocument>();
            modelBuilder.Entity<PassportDocument>();
        }
    }
}
