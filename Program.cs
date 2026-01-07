using ksi.Interfaces;
using ksi.Repository;
using KSI_Project.Helpers.DbContexts;
using KSI_Project.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext
builder.Services.AddDbContext<ksiDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySQLConnectionString"),
        new MySqlServerVersion(new Version(8, 0, 36))
    )
);

// Register repositories
builder.Services.AddScoped<ICanteenRepository, CanteenRepository>();     
builder.Services.AddScoped<IWebsiteRepository, WebsiteRepository>();
builder.Services.AddScoped<IEventDetailsRepository, EventDetailsRepository>();
builder.Services.AddScoped<ITimetableRepository, TimetableRepository>();
builder.Services.AddScoped<IFacultyRepository, FacultyRepository>();

// Add MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Default route - Set Website as default for users
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Canteen}/{action=Canteens}/{id?}");
//Uncomment your original route and change to a proper action
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Faculty}/{action=FacultyDetails}/{id?}");

app.Run();