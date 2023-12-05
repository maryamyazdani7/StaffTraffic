using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StaffTraffic.Areas.Identity.Data;
using StaffTraffic.Data;
using StaffTraffic.DataAccess;
using System;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ApplicationContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationContextConnection' not found.");

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<Role>()
    .AddEntityFrameworkStores<ApplicationContext>();


builder.Services.AddScoped<TrafficService>();
builder.Services.AddScoped<UserService>();
// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation();
var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        UserManager<ApplicationUser> userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        RoleManager<Role> roleManager = services.GetRequiredService<RoleManager<Role>>();

        // Create roles if they don't exist
        string[] roleNames = { "Admin", "User" };
        IdentityResult roleResult;

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if (!roleExist)
            {
                // Create the role and seed it with the admin user
                roleResult = await roleManager.CreateAsync(new Role { Name = roleName });
            }
        }

        // Create an admin user
        var adminUser = new ApplicationUser
        {
            UserName = "140209",
            Email = "admin@example.com",
            FirstName = "Admin",
            LastName = "Admin",
            IsEnable = true
        };

        string adminPassword = "Admin@123";

        var user = await userManager.FindByEmailAsync(adminUser.Email);

        if (user == null)
        {
            // If the admin user doesn't exist, create it
            var createAdminUserResult = await userManager.CreateAsync(adminUser, adminPassword);

            if (createAdminUserResult.Succeeded)
            {
                // Assign the admin user to the "Admin" role
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while creating roles and admin user.");
    }
}

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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
