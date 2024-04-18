using Faculty_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Faculty_Service.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Abiturient> Abiturients { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<NextLevel> NextLevels { get; set; }
        public DbSet<EducationDocumentType> EducationDocumentTypes { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<EducationProgram> Programs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Abiturient>();
            modelBuilder.Entity<Level>();
            modelBuilder.Entity<NextLevel>(options =>
            {
                options.HasOne(p => p.level).WithMany(t => t.nextLevels).HasForeignKey(p => p.levelId);
                //options.HasOne(p => p.nextLevel).WithMany(t => t.nextLevels).HasForeignKey(p => p.nextLevelId);
            });
            modelBuilder.Entity<EducationDocumentType>(options =>
            {
                options.HasOne(p => p.level).WithMany(t => t.educationDocumentTypes).HasForeignKey(p => p.levelId);
            });
            modelBuilder.Entity<Faculty>();
            modelBuilder.Entity<EducationProgram>(options =>
            {
                options.HasOne(p => p.faculty).WithMany(t => t.programs).HasForeignKey(p => p.facultyId);
            });
        }
    }
}
