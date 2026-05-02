using StudentWeb.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<StudentAuthService>();
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
