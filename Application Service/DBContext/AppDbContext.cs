using Application_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Application_Service.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Abiturient> Abiturients { get; set; }
        public DbSet<EducationProgram> Programs { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<ApplicationProgram> ApplicationPrograms { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Abiturient>();
            modelBuilder.Entity<EducationProgram>();
            modelBuilder.Entity<Application>(options =>
            {
                options.HasOne(p => p.abiturient).WithOne(p => p.application).HasForeignKey<Application>(p => p.id);
            });
            modelBuilder.Entity<ApplicationProgram>(options =>
            {
                options.HasOne(p => p.application).WithMany(p => p.applicationPrograms).HasForeignKey(p => p.id);

                options.HasOne(p => p.educationProgram).WithMany(p => p.applicationPrograms).HasForeignKey(p => p.programId);
            });
        }
    }
}
