using KSI_Project.Interfaces;
using KSI_Project.Repositories;
using KSI_Project.Helpers.DbContexts;
using KSI_Project.Repository;
using Microsoft.EntityFrameworkCore;
using KSI_Project.Repository.Interfaces;
using KSI_Project.Repository.Implementations;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ksiDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MySQLConnectionString"),
        new MySqlServerVersion(new Version(8, 0, 29))
    )
);
builder.Services.AddScoped<IEventDetailsRepository, EventDetailsRepository>();
builder.Services.AddScoped<ICGPACalculationRepository, CGPACalculationRepository>();
builder.Services.AddScoped<ISyllabusRepository, SyllabusRepository>();
builder.Services.AddScoped<ITimetableRepository, TimetableRepository>();
//builder.Services.AddScoped<IFacultySupportRepository, FacultySupportRepository>();
//builder.Services.AddScoped<IPlacementSupportRepository, PlacementSupportRepository>();
//builder.Services.AddScoped<IIDBalanceRepository, IDBalanceRepository>();
builder.Services.AddControllersWithViews();

var app = builder.Build();
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
