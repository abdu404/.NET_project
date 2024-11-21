//using ContactManager.Models;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity;
//using System;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddRouting(options =>
//{
//    options.LowercaseUrls = true;
//    options.AppendTrailingSlash = true;
//});

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//builder.Services.AddDbContext<ContactDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("ContactManager")));

//builder.Services.AddIdentity<AppUser, IdentityRole>(
//    options =>
//    {
//        options.Password.RequiredUniqueChars = 0;
//        options.Password.RequireUppercase = false;
//        options.Password.RequiredLength = 7;
//        options.Password.RequireNonAlphanumeric = false;
//        options.Password.RequireLowercase = false;
//    })
//    .AddEntityFrameworkStores<ContactDbContext>().AddDefaultTokenProviders();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=MainPage}/{id?}/{slug?}");

//app.Run();
using ContactManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ContactDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ContactManager")));

builder.Services.AddIdentity<AppUser, IdentityRole>(
    options =>
    {
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 7;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<ContactDbContext>().AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.SlidingExpiration = true;
});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; // Required for GDPR compliance
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=MainPage}/{id?}/{slug?}");

app.Run();
