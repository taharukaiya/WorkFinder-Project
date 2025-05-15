using System.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WorkFinder.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var provider = builder.Services.BuildServiceProvider();
var config = provider.GetRequiredService<IConfiguration>();
builder.Services.AddDbContext<HhandDbContext>(item => item.UseSqlServer(config.GetConnectionString("dbcs")));


// (((for session ig 
// Add services to the container.
builder.Services.AddControllersWithViews();

// 👇 Add this line
//builder.Services.AddSession();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// session ends))))
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Register Authentication with Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";  // Redirect to login page if unauthenticated
        options.AccessDeniedPath = "/Account/AccessDenied";  // Redirect to access denied page if user has no rights
    });




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // added by me
    app.UseHsts();
}


// by me 
app.UseHttpsRedirection();
//
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",

    pattern: "{controller=Home}/{action=Index}/{id?}"
);


//app.UseSession();

app.Run();




