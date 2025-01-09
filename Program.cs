using Services;
using System;
using System.IO;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IStorageService, StorageService>();
builder.Services.AddTransient<IAuthService, AuthService>();

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