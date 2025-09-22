using KSI_Project.Models.Entity;
using Microsoft.EntityFrameworkCore;


namespace KSI_Project.Helpers.DbContexts
{
    public class ksiDbContext : DbContext
    {
        public ksiDbContext(DbContextOptions<ksiDbContext> options) : base(options) { }

        public DbSet<EventDetails> EventDetails { get; set; }
        public DbSet<StudentTimetable> StudentTimetables { get; set; }
        public DbSet<Syllabus> Syllabi { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
