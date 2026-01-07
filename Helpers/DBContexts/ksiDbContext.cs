using ksi.Models;
using ksi.Models.Entity;
using KSI.Models.Entity;
using ksi_project.Models.Entity;
using KSI_Project.Models.Entity;
using KsiProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace KSI_Project.Helpers.DbContexts
{
    public class ksiDbContext : DbContext
    {
        public ksiDbContext(DbContextOptions<ksiDbContext> options) : base(options) { }

        public DbSet<StudentProfile> student_profile { get; set; }
        public DbSet<syllabus> syllabus { get; set; }
        public DbSet<mstEventDetails> mstEventDetails { get; set; }
        public DbSet<mstBatch> mstBatch { get; set; }
        public DbSet<mstDepartment> mstDepartment { get; set; }
        public DbSet<mstSection> mstSection { get; set; }
        public DbSet<mstSubject> mstSubject { get; set; }
        public DbSet<mstClubs> mstClubs { get; set; }
        public DbSet<timetable> timetable { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<mstCanteen> mstCanteens { get; set; }
        public DbSet<CanteenId> mstCanteenIds { get; set; }
        public DbSet<mstTimetable> mstTimetable { get; set; }


        public DbSet<mstFaculty> Faculties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Faculty configuration
            modelBuilder.Entity<mstFaculty>(entity =>
            {
                entity.HasKey(e => e.FacultyID);
                entity.Property(e => e.FacultyID).ValueGeneratedOnAdd();
                entity.Property(e => e.IsActive).HasDefaultValue(true);
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.HasIndex(e => e.IsActive);
            });
            // Canteen -> CanteenId relationship
            modelBuilder.Entity<mstCanteen>()
                .HasOne(c => c.CanteenDetails)
                .WithMany(ci => ci.Canteens)
                .HasForeignKey(c => c.CanteenID)
                .OnDelete(DeleteBehavior.Restrict);
        }



    }
}