using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using OnlineCoursesPlatform.Data;
using OnlineCoursesPlatform.Models;
using OnlineCoursesPlatform.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<IEmailSender, DummyEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();

    string roleName = "Admin";
    string adminEmail = "tsvetan.velev.highschool@buditel.bg";

    if (!await roleManager.RoleExistsAsync(roleName))
    {
        await roleManager.CreateAsync(new IdentityRole(roleName));
    }

    var user = await userManager.FindByEmailAsync(adminEmail);
    if (user == null)
    {
        var adminUser = new AppUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        string adminPassword = "Admin123!";
        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, roleName);
        }
    }
    else if (!(await userManager.IsInRoleAsync(user, roleName)))
    {
        await userManager.AddToRoleAsync(user, roleName);
    }

    // --- LECTURER SETUP ---

    string lecturerEmail = "lecturer@platform.bg";
    string lecturerPassword = "Lecturer123!";

    if (!await roleManager.RoleExistsAsync("Lecturer"))
    {
        await roleManager.CreateAsync(new IdentityRole("Lecturer"));
    }

    var lecturerUser = await userManager.FindByEmailAsync(lecturerEmail);
    if (lecturerUser == null)
    {
        var newLecturer = new AppUser
        {
            UserName = lecturerEmail,
            Email = lecturerEmail,
            EmailConfirmed = true,
            FullName = "Demo Lecturer",
            Role = "Lecturer"

        };

        var result = await userManager.CreateAsync(newLecturer, lecturerPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newLecturer, "Lecturer");
        }
    }


}

app.Run();
