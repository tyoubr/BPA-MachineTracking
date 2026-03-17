using BPAMatchineTrack.Models;
using DevExpress.AspNetCore;
using DevExpress.XtraCharts;
using DevExpress.XtraReports.Web.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);


// ==============================
// Database Connection
// ==============================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<CottonclubContext>(options =>
    options.UseSqlServer(connectionString));


// ==============================
// Identity Configuration
// ==============================
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
})
.AddEntityFrameworkStores<CottonclubContext>()
.AddDefaultTokenProviders();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


// ==============================
// MVC & DevExpress
// ==============================
builder.Services.AddDevExpressControls();

builder.Services.AddControllersWithViews();

builder.Services.AddMvc()
    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


// ==============================
// Build Application
// ==============================
var app = builder.Build();


// ==============================
// Seed Identity Roles
// ==============================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await IdentitySeeder.SeedRolesAsync(services);
}


// ==============================
// Environment Setup
// ==============================
var env = app.Services.GetRequiredService<IWebHostEnvironment>();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


// ==============================
// Static Files
// ==============================
app.UseHttpsRedirection();

app.UseStaticFiles();

//app.UseStaticFiles(new StaticFileOptions
//{
//    FileProvider = new PhysicalFileProvider(
//        Path.Combine(env.ContentRootPath, "node_modules")),
//    RequestPath = "/node_modules"
//});


// ==============================
// Middleware Pipeline
// ==============================
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseDevExpressControls();


// ==============================
// Routing
// ==============================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.MapRazorPages();


// ==============================
app.Run();