using ksi.Data.Repository;
using ksi.Interfaces;
using ksi.Repositories;
using ksi.Repository;
using KSI_Project.Helpers.DbContexts;
using KSI_Project.Models.Entity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ── DbContext ─────────────────────────────────────────────────────────
builder.Services.AddDbContext<ksiDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySQLConnectionString"),
        new MySqlServerVersion(new Version(8, 0, 36))
    )
);

// ── Repository registrations ──────────────────────────────────────────
builder.Services.AddScoped<ICanteenRepository, CanteenRepository>();
builder.Services.AddScoped<IWebsiteRepository, WebsiteRepository>();
builder.Services.AddScoped<IEventDetailsRepository, EventDetailsRepository>();
builder.Services.AddScoped<ITimetableRepository, TimetableRepository>();
builder.Services.AddScoped<IFacultyRepository, FacultyRepository>();
builder.Services.AddScoped<iSyllabusRepository, SyllabusRepository>();
builder.Services.AddScoped<IHallLocatorRepository, HallLocatorRepository>();   // Hall Locator

// ── MVC ───────────────────────────────────────────────────────────────
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// ── Build ─────────────────────────────────────────────────────────────
var app = builder.Build();

// ── Middleware ────────────────────────────────────────────────────────
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// ── Routes ────────────────────────────────────────────────────────────
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=AdminHallLocator}/{action=Index}/{id?}");   // keep Home as default

app.MapRazorPages();

app.Run();