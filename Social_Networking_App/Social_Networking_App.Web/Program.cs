using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Social_Networking_App.Core.Domains.IRepositories;
using Social_Networking_App.Core.IServices;
using Social_Networking_App.Core.Services;
using Social_Networking_App.Infrastructure.DbContext;
using Social_Networking_App.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
    {
        options.LoginPath = $"/Identity/Account/Login"; // Redirect to this path when not authenticated
        options.LogoutPath = $"/Identity/Account/Logout";
        options.AccessDeniedPath = $"/Identity/Account/AccessDenied"; // Redirect if access is denied     
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Optional: sets cookie expiry
});


builder.Services.AddRazorPages();



// Notification
builder.Services.AddNotyf(config => { config.DurationInSeconds = 15; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });

//Services
builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
builder.Services.AddScoped<IProfileRepo, ProfileRepo>();
builder.Services.AddScoped<IEventRepo, EventRepo>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IEventService, EventService>();



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
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}");    

app.Run();
