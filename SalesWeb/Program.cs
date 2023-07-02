using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWeb.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SalesWebContext>(options =>
    options.UseMySql("Server=localhost;uid=root;password=123456;database=saleswebmvcappdb", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql")));

//builder.Services.AddScoped<SeedingService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

WebApplication app = builder.Build();


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

//Seed database

SeedingService.Seed(app);

app.Run();
