using KSI_Project.Models.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace KSI_Project.Helpers.DbContexts
{
    public class kctDbContext : DbContext
    {
        public kctDbContext(DbContextOptions<kctDbContext> options) : base(options) { }

        public DbSet<EventDetails> EventDetails { get; set; }
    }
}
