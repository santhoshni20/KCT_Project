using KSI_Project.Models.Entity;
using ksiProject.Models;
using Microsoft.EntityFrameworkCore;

namespace KSI_Project.Helpers.DbContexts
{
    public class ksiDbContext : DbContext
    {
        public ksiDbContext(DbContextOptions<ksiDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<IdBalance> IdBalances { get; set; }
        public DbSet<PlacementDetail> PlacementDetails { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<Timetable> Timetables { get; set; }
        public DbSet<TeacherTimetable> TeacherTimetables { get; set; }
        public DbSet<Canteen> Canteens { get; set; }
        public DbSet<CanteenInfo> CanteenInfos { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}
