//using ksi.Interfaces;
//using ksi.Repository;
//using KSI.Interfaces;
//using KSI.Repositories;
//using ksi_project.Interfaces;
//using ksi_project.Repositories;
//using ksi_project.Repository.Implementation;
//using ksi_project.Repository.Interface;
using KSI_Project.Helpers.DbContexts;
//using KSI_Project.Interfaces;
//using KSI_Project.Repositories;
//using KsiProject.Interfaces;
//using KsiProject.Repositories;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ksiDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySQLConnectionString"),
        new MySqlServerVersion(new Version(8, 0, 36))
    )
);
//builder.Services.AddScoped<ICanteenRepository, CanteenRepository>();
//builder.Services.AddScoped<ICGPACalculationRepository, CGPACalculationRepository>();
//builder.Services.AddScoped<IEventDetailsRepository, EventDetailsRepository>();
//builder.Services.AddScoped<ICGPACalculationRepository, CGPACalculationRepository>();
//builder.Services.AddScoped<ISyllabusRepository, SyllabusRepository>();
//builder.Services.AddScoped<ITimetableRepository, TimetableRepository>();
//builder.Services.AddScoped<IStudentRepository, StudentRepository>();
//builder.Services.AddScoped<IFacultySupportRepository, FacultySupportRepository>();
//builder.Services.AddScoped<IEventDetailsRepository, EventDetailsRepository>();
//builder.Services.AddScoped<ITimetableRepository, TimetableRepository>();
//builder.Services.AddScoped<ICGPARepository, CGPARepository>();
//builder.Services.AddScoped<ISyllabusRepository, SyllabusRepository>();
//builder.Services.AddScoped<IPlacementSupportRepository, PlacementSupportRepository>();
//builder.Services.AddScoped<IWebsiteRepository, WebsiteRepository>();


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

