using OnlineShop.Data;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Services;
using OnlineShop.Services.Implementations;
using OnlineShop.Filters;
using Microsoft.Extensions.DependencyInjection;

const string DEFAULT_DB_CONNECTION_KEY = "DefaultConnection";
var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<OnlineShopDBContext>(options => options.UseSqlServer(configuration.GetConnectionString(DEFAULT_DB_CONNECTION_KEY)));
builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserService>(provider =>
{
    var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();

    return new UserService(
        configuration["SessionSettings:CurrentUserLoginKey"],
        configuration["SessionSettings:CurrentUserRoleKey"],
        httpContextAccessor
    );
});


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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=Index}/{id?}");

app.Run();
