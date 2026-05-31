using Microsoft.AspNetCore.Authentication.Cookies;
using Shared.Data;
using StudentWeb.Services;

var builder = WebApplication.CreateBuilder(args);
var databasePath = builder.Configuration["Database:FileName"];

// 1. Register Shared Infrastructure & Auth Services
builder.Services.AddSingleton(_ => new DatabaseHelper(databasePath: databasePath));
builder.Services.AddScoped<StudentAuthService>();

// 2. Configure Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
    });

// 3. Register Existing Web Services
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ProfileService>();
builder.Services.AddScoped<JobBrowseService>();
builder.Services.AddScoped<ResumeBuilderService>();
builder.Services.AddScoped<NotificationQueryService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 4. Authentication must come AFTER UseRouting and BEFORE UseAuthorization / MapControllerRoute
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
