using Hospital.Entityframework.Contexts;
using Hostpital.Service.IServices;
using Hostpital.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<HospitalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Hospital")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IGeographyService, GeographyService>();
builder.Services.AddScoped<IPatientService, PatientService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "province",
    pattern: "{controller=Province}/{action=Index}/{code?}");

app.MapControllerRoute(
    name: "district",
    pattern: "{controller=District}/{action=Index}/{code?}");

app.MapControllerRoute(
    name: "ward",
    pattern: "{controller=Ward}/{action=Index}/{code?}");

app.MapControllerRoute(
    name: "patient",
    pattern: "{controller=Patient}/{action=Index}/{code?}");

app.Run();
