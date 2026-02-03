using ksi.Data.Repository;
using ksi.Interfaces;
using ksi.Repositories;
using ksi.Repository;
using KSI_Project.Helpers.DbContexts;
using KSI_Project.Models.Entity;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddScoped<iSyllabusRepository, SyllabusRepository>();
builder.Services.AddScoped<IHallLocatorRepository, HallLocatorRepository>();

//// Add MVC and Razor Pages
//builder.Services.AddControllersWithViews()
//    .AddRazorRuntimeCompilation(); // Optional: Enables hot reload for views

builder.Services.AddRazorPages();

// BUILD THE APP - This must come BEFORE using 'app'
var app = builder.Build();

// Middleware Configuration
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage(); // Shows detailed errors in development
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Map routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=AdminHallLocator}/{action=HallResult}/{id?}"); // Changed default to HallLocator

app.MapRazorPages();

app.Run();
