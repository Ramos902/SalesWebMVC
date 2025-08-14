using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Internal;
using SalesWebMVC.Data;
var builder = WebApplication.CreateBuilder(args);
try
{
    builder.Services.AddDbContext<SalesWebMVCContext>(options =>
        options.UseMySql(builder.Configuration.GetConnectionString("SalesWebMVCContext"),
        new MySqlServerVersion(new Version(8, 0, 43)),
        MySqlOptions => MySqlOptions.MigrationsAssembly("SalesWebMVC")));
}
catch (InvalidOperationException)
{
    throw new InvalidOperationException("Connection string 'SalesWebMVCContext' not found.");
}

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.Run();
