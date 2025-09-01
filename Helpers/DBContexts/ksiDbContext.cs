using KSI_Project.Models.Entity;
using KSI_Project.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace KSI_Project.Helpers.DbContexts
{
    public class ksiDbContext : DbContext
    {
        public ksiDbContext(DbContextOptions<ksiDbContext> options) : base(options) { }

        public DbSet<EventDetails> EventDetails { get; set; }
        public DbSet<Timetable> Timetables { get; set; }
        public DbSet<AlumniDetails> AlumniDetails { get; set; }
        public DbSet<FacultyDetails> FacultyDetails { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<SyllabusFile> SyllabusFiles { get; set; }
    }
}
