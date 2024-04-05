using FluentValidation;
using Hospital.Domain.DTO;
using Hospital.Domain.Validations;
using Hospital.Entityframework.Contexts;
using Hospital.Web;
using Hospital.Web.Customs;
using Hostpital.Service.IServices;
using Hostpital.Service.Services;
using Microsoft.EntityFrameworkCore;
using PdfSharp.Fonts;

var builder = WebApplication.CreateBuilder(args);

GlobalFontSettings.FontResolver = new CustomFontResolver();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<HospitalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Hospital")));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddScoped<IGeographyService, GeographyService>();
builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddScoped<IValidator<PatientCreateDto>, PatientCreateValidator>();
builder.Services.AddScoped<IValidator<PatientUpdateDto>, PatientUpdateValidator>();

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

app.MapControllers();

app.Run();
