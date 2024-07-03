using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PermissionsManagement.Constants;
using PermissionsManagement.Data;

namespace PermissionsManagement.Seeds;

public class AppDbSeeder
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _appDbContext;

    public AppDbSeeder(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext applicationDbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _appDbContext = applicationDbContext;
    }

    public static async Task SeedUserAsync(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (!userManager.Users.Any(p => p.UserName == "admin"))
            {
                IdentityUser user = new()
                {
                    UserName = "admin",
                    Email = "admin@admin.com",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                userManager.CreateAsync(user, "1").Wait();
            }
            if (!userManager.Users.Any(p => p.UserName == "superadmin"))
            {
                IdentityUser user = new()
                {
                    UserName = "superadmin",
                    Email = "superadmin@admin.com",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                userManager.CreateAsync(user, "1").Wait();
            }
            if (!userManager.Users.Any(p => p.UserName == "basic"))
            {
                IdentityUser user = new()
                {
                    UserName = "basic",
                    Email = "basic@admin.com",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                userManager.CreateAsync(user, "1").Wait();
            }
        }
    }

    public static async Task SeedRolesAsync(WebApplication app)
    {
        using (var scoped = app.Services.CreateScope())
        {
            var roleManager = scoped.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if (!roleManager.Roles.Any(p => p.Name == "Admin"))
            {
                IdentityRole role = new IdentityRole
                {
                    Name = Roles.Admin.ToString(),
                };
                await roleManager.CreateAsync(role);
            }
            if (!roleManager.Roles.Any(p => p.Name == "SuperAdmin"))
            {
                IdentityRole role = new IdentityRole
                {
                    Name = Roles.SuperAdmin.ToString(),
                };
                await roleManager.CreateAsync(role);
            }
            if (!roleManager.Roles.Any(p => p.Name == "Basic"))
            {
                IdentityRole role = new IdentityRole
                {
                    Name = Roles.Basic.ToString(),
                };
                await roleManager.CreateAsync(role);
            }
        }
    }
}
