using KSI_Project.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace KSI_Project.Helpers.DbContexts
{
    public class ksiDbContext : DbContext
    {
        public ksiDbContext(DbContextOptions<ksiDbContext> options) : base(options) { }

        // 🔹 Existing database tables
        public DbSet<EventDetails> EventDetails { get; set; }
        public DbSet<StudentTimetable> StudentTimetables { get; set; }
        public DbSet<Syllabus> Syllabi { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<User> Users { get; set; }

        // 🔹 New Canteen tables
        public DbSet<Canteen> Canteens { get; set; }
        public DbSet<CanteenId> CanteenIds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ Configure CanteenId table
            modelBuilder.Entity<CanteenId>(entity =>
            {
                entity.ToTable("canteenId");
                entity.HasKey(e => e.CanteenID);
                entity.Property(e => e.CanteenName).IsRequired().HasMaxLength(100);

                // Optional: audit fields
                entity.Property(e => e.CreatedBy).HasMaxLength(100);
                entity.Property(e => e.UpdatedBy).HasMaxLength(100);
                entity.Property(e => e.DeletedBy).HasMaxLength(100);
            });

            // ✅ Configure Canteen table (ONLY ONCE!)
            modelBuilder.Entity<Canteen>(entity =>
            {
                entity.ToTable("canteen");
                entity.HasKey(e => e.ItemID);
                entity.Property(e => e.DishName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Availability).HasMaxLength(100);
                entity.Property(e => e.Price).HasColumnType("decimal(8,2)");

                // Relationship: one CanteenId → many Canteen dishes
                entity.HasOne(c => c.CanteenDetails)
                      .WithMany(ci => ci.Canteens)
                      .HasForeignKey(c => c.CanteenID)
                      .OnDelete(DeleteBehavior.Cascade);

                // Optional: audit fields
                entity.Property(e => e.CreatedBy).HasMaxLength(100);
                entity.Property(e => e.UpdatedBy).HasMaxLength(100);
                entity.Property(e => e.DeletedBy).HasMaxLength(100);
            });

            // ✅ Seed the 6 canteens (initial data)
            modelBuilder.Entity<CanteenId>().HasData(
                new CanteenId
                {
                    CanteenID = 1,
                    CanteenName = "Ratio",
                    IsActive = true,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now
                },
                new CanteenId
                {
                    CanteenID = 2,
                    CanteenName = "Munch Box",
                    IsActive = true,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now
                },
                new CanteenId
                {
                    CanteenID = 3,
                    CanteenName = "CCD",
                    IsActive = true,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now
                },
                new CanteenId
                {
                    CanteenID = 4,
                    CanteenName = "Main Core",
                    IsActive = true,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now
                },
                new CanteenId
                {
                    CanteenID = 5,
                    CanteenName = "East Core",
                    IsActive = true,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now
                },
                new CanteenId
                {
                    CanteenID = 6,
                    CanteenName = "Namma Cafe",
                    IsActive = true,
                    CreatedBy = "Admin",
                    CreatedDate = DateTime.Now
                }
            );
        }
        public DbSet<Faculty> Faculties { get; set; }



    }
}