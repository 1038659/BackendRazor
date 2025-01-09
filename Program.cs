using Services;
using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore; // Ensure this line is present
using Services;
using System.Text.Json.Serialization; // Add this line

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });
builder.Services.AddTransient<IStorageService, StorageService>();
builder.Services.AddTransient<IAuthService, AuthService>();

// Register DatabaseContext
builder.Services.AddDbContext<Models.DatabaseContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add authentication services
builder.Services.AddAuthentication("CookieAuthentication")
    .AddCookie("CookieAuthentication", options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

// Add session services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

// Enable session middleware
app.UseSession();

// Enable authentication middleware
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

AppDomain.CurrentDomain.AssemblyResolve += (sender, eventArgs) =>
{
    var assemblyName = new AssemblyName(eventArgs.Name).Name + ".dll";
    var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName);
    return File.Exists(path) ? Assembly.LoadFrom(path) : null;
};

app.Run("http://localhost:5000");