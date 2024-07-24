using OnlineShop.Data;
using Microsoft.EntityFrameworkCore;

const string DEFAULT_DB_CONNECTION_KEY = "DefaultConnection";
var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<OnlineShopDBContext>(options => options.UseSqlServer(configuration.GetConnectionString(DEFAULT_DB_CONNECTION_KEY)));
builder.Services.AddMemoryCache();

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
    pattern: "{controller=Books}/{action=Index}/{id?}");

app.Run();
