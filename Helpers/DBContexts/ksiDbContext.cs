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
        public DbSet<timetable> timetable { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Canteen> Canteens { get; set; }
        public DbSet<CanteenId> CanteenIds { get; set; }
    }
}