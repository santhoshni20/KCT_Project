using Microsoft.EntityFrameworkCore;
using KSI_Project.Helpers.DbContexts;
using KSI_Project.Interfaces;
using KSI_Project.Repositories;

var builder = WebApplication.CreateBuilder(args);

// ? Register EF Core + MySQL
builder.Services.AddDbContext<kctDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySQLConnectionString"),
        new MySqlServerVersion(new Version(8, 0, 29))
    )
);

// ? Register Repositories
builder.Services.AddScoped<IEventDetailsRepository, EventDetailsRepository>();

// ? Add MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ? Configure HTTP pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
