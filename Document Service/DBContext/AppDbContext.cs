using Document_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Document_Service.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Abiturient> Abiturients { get; set; }
        public DbSet<EducationDocument> Educations { get; set; }
        public DbSet<PassportDocument> Passports { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Abiturient>();
            modelBuilder.Entity<EducationDocument>(options =>
            {
                options.HasOne(p => p.abiturient).WithOne(t => t.educationDocument).HasForeignKey<EducationDocument>(p => p.id);
            });
            modelBuilder.Entity<PassportDocument>(options =>
            {
                options.HasOne(p => p.abiturient).WithOne(t => t.passportDocument).HasForeignKey<PassportDocument>(p => p.id);
            });
        }
    }
}
