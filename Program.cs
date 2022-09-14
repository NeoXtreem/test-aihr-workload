using AIHR.Test.Converters;
using AIHR.Test.Data;
using AIHR.Test.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var dbPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Workload.db");

builder
    .Services.AddControllersWithViews()
    .Services.AddDbContext<WorkloadContext>(options => options.UseSqlite($"Data Source={dbPath}"))
    .AddScoped<StudentsRepository>()
    .AddScoped<CoursesRepository>()
    .AddScoped<WorkloadCalculatorService>();

builder.Services.Configure<JsonOptions>(options => options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter()));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    DbInitializer.Initialize(scope.ServiceProvider.GetRequiredService<WorkloadContext>());
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();