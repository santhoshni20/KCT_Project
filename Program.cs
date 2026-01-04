using ksi.Interfaces;                        // For IEventDetailsRepository
using ksi.Repository;                         // For EventDetailsRepository implementation
using KSI_Project.Helpers.DbContexts;        // For ksiDbContext
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Configure DbContext
builder.Services.AddDbContext<ksiDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySQLConnectionString"),
        new MySqlServerVersion(new Version(8, 0, 36))
    )
);

// ✅ Register repositories (DI)
builder.Services.AddScoped<IEventDetailsRepository, EventDetailsRepository>();

// ✅ Add MVC controllers with views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// ✅ Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// ✅ Default route
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=EventDetails}/{action=AddEvents}/{id?}"
);

app.MapControllers();

app.Run();
