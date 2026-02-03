using ksi.Models;
using ksi.Models.Entity;
using KSI.Models.Entity;
using ksi_project.Models.Entity;
using KSI_Project.Models.Entity;
using KsiProject.Entities;
using Microsoft.EntityFrameworkCore;
using YourProject.Entities;

namespace KSI_Project.Helpers.DbContexts
{
    public class ksiDbContext : DbContext
    {
        public ksiDbContext(DbContextOptions<ksiDbContext> options) : base(options) { }

        // Existing DbSets
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
        public DbSet<mstSyllabus> mstSyllabus { get; set; }

        // Hall Locator DbSets
        public DbSet<mstBlock> mstBlock { get; set; }
        public DbSet<mstRoom> mstRoom { get; set; }
        public DbSet<HallLocatorEntities> HallSeating { get; set; }

        public DbSet<mstFaculty> Faculties { get; set; }
        public DbSet<mstSyllabus> mstSyllabus { get; set; }
        //public DbSet<mstSubject> mstSubject { get; set; }
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

            // Hall Locator relationships
            modelBuilder.Entity<mstRoom>()
                .HasOne<mstBlock>()
                .WithMany()
                .HasForeignKey(r => r.blockId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HallLocatorEntities>()
                .HasOne<mstRoom>()
                .WithMany()
                .HasForeignKey(s => s.roomId)
                .OnDelete(DeleteBehavior.Restrict);

            // SEED DATA FOR BLOCKS
            modelBuilder.Entity<mstBlock>().HasData(
                new mstBlock
                {
                    blockId = 1,
                    blockName = "Admin Block",
                    isActive = true,
                    createdBy = 1,
                    createdDate = DateTime.Now
                },
                new mstBlock
                {
                    blockId = 2,
                    blockName = "A Block",
                    isActive = true,
                    createdBy = 1,
                    createdDate = DateTime.Now
                },
                new mstBlock
                {
                    blockId = 3,
                    blockName = "B Block",
                    isActive = true,
                    createdBy = 1,
                    createdDate = DateTime.Now
                },
                new mstBlock
                {
                    blockId = 4,
                    blockName = "C Block",
                    isActive = true,
                    createdBy = 1,
                    createdDate = DateTime.Now
                },
                new mstBlock
                {
                    blockId = 5,
                    blockName = "E Block",
                    isActive = true,
                    createdBy = 1,
                    createdDate = DateTime.Now
                },
                new mstBlock
                {
                    blockId = 6,
                    blockName = "F Block",
                    isActive = true,
                    createdBy = 1,
                    createdDate = DateTime.Now
                }
            );
        }
    }
}