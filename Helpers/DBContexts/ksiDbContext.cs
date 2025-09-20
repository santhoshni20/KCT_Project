using KSI_Project.Models.Entity;
using Microsoft.EntityFrameworkCore;


namespace KSI_Project.Helpers.DbContexts
{
    public class ksiDbContext : DbContext
    {
        public ksiDbContext(DbContextOptions<ksiDbContext> options) : base(options) { }

        public DbSet<EventDetails> EventDetails { get; set; }
    }
}
