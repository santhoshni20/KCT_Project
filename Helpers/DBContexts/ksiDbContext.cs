using KSI_Project.Models.Entity;
using ksiProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace KSI_Project.Helpers.DbContexts
{
    public class ksiDbContext : DbContext
    {
        public ksiDbContext(DbContextOptions<ksiDbContext> options) : base(options) { }
        // 1. Student
        public DbSet<Student> Students { get; set; }

        // 2. Department
        public DbSet<Department> Departments { get; set; }

        // 3. Courses
        public DbSet<Course> Courses { get; set; }

        // 4. Teachers
        public DbSet<Teacher> Teachers { get; set; }

        // 5. ID Balance
        public DbSet<IdBalance> IdBalances { get; set; }

        // 6. Placement Details
        public DbSet<PlacementDetail> PlacementDetails { get; set; }

        // 7. Batch
        public DbSet<Batch> Batches { get; set; }

        // 8. Timetable
        public DbSet<Timetable> Timetables { get; set; }

        // 9. Teachers Timetable
        public DbSet<TeacherTimetable> TeacherTimetables { get; set; }

        // 10. Canteen Menu
        public DbSet<Canteen> Canteens { get; set; }

        // 11. Canteen Info (Canteen_ID)
        public DbSet<CanteenInfo> CanteenInfos { get; set; }

        // 12. Event
        public DbSet<Event> Events { get; set; }

    }
}
