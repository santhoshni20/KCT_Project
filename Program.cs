using KSI_Project.Interfaces;
using KSI_Project.Repositories;
using KSI_Project.Helpers.DbContexts;
using Microsoft.EntityFrameworkCore;
using KSI_Project.Repository.Implementations;
using KSI_Project.Repository.Interfaces;
using KSI_Project.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ksiDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySQLConnectionString"),
        new MySqlServerVersion(new Version(8, 0, 36))
    )
);
builder.Services.AddScoped<ICanteenRepository, CanteenRepository>();
builder.Services.AddScoped<IEventDetailsRepository, EventDetailsRepository>();
builder.Services.AddScoped<ICGPACalculationRepository, CGPACalculationRepository>();
builder.Services.AddScoped<ISyllabusRepository, SyllabusRepository>();
builder.Services.AddScoped<ITimetableRepository, TimetableRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IFacultySupportRepository, FacultySupportRepository>();


builder.Services.AddControllersWithViews();

var app = builder.Build();

// ✅ Middleware setup
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
/*
// ✅ Default route (Canteen Index)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");*/
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();

