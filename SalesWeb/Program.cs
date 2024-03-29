using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWeb.Data;
using SalesWeb.Services;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using MySql.Data.MySqlClient;


var builder = WebApplication.CreateBuilder(args);


string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<SalesWebContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

IServiceCollection serviceCollection = builder.Services.AddScoped<SellerService>();
serviceCollection.AddScoped<DepartmentService>();
serviceCollection.AddScoped<SalesRecordService>();

var ptBR = new CultureInfo("pt-BR");
var localizationOptions = new RequestLocalizationOptions
{   
    DefaultRequestCulture = new RequestCulture(ptBR),
    SupportedCultures = new List<CultureInfo> { ptBR },
    SupportedUICultures = new List<CultureInfo> { ptBR }
};


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

app.UseRequestLocalization(localizationOptions);

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


