//using ksi.Interfaces;                        // For IEventDetailsRepository
//using ksi.Repository;                         // For EventDetailsRepository implementation
//using ksi_project.Repository.Implementation;
//using ksi_project.Repository.Interface;
//using KSI_Project.Helpers.DbContexts;        // For ksiDbContext
//using KSI_Project.Repositories;
//using Microsoft.EntityFrameworkCore;

//var builder = WebApplication.CreateBuilder(args);

//// ✅ Configure DbContext
//builder.Services.AddDbContext<ksiDbContext>(options =>
//    options.UseMySql(
//        builder.Configuration.GetConnectionString("MySQLConnectionString"),
//        new MySqlServerVersion(new Version(8, 0, 36))
//    )
//);

//// ✅ Register repositories (DI)
//builder.Services.AddScoped<IEventDetailsRepository, EventDetailsRepository>();
//builder.Services.AddScoped<ICanteenRepository, CanteenRepository>();
//// ✅ Add MVC controllers with views
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// ✅ Middleware
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();
//app.UseAuthorization();

//// ✅ Default route
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=CanteenController}/{action=Index}/{id?}"
//);

//app.MapControllers();

//app.Run();
// Program.cs
// Program.cs
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
    pattern: "{controller=Website}/{action=Canteen}/{id?}");

app.Run();