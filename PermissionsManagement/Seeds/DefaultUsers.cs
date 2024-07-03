using Microsoft.AspNetCore.Identity;
using PermissionsManagement.Constants;
using System.Security.Claims;

namespace PermissionsManagement.Seeds;

public static class DefaultUsers
{

    public static async Task SeedBasicUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        //Seed Default User
        var defaultUser = new IdentityUser
        {
            UserName = "basicuser@gmail.com",
            Email = "basicuser@gmail.com",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
        };
        if (userManager.Users.All(u => u.Id != defaultUser.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "1q2w3E*!");
                await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
            }
        }
    }
    public static async Task SeedTestUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        //Seed Default User
        var defaultUser = new IdentityUser
        {
            UserName = "testuser@gmail.com",
            Email = "testuser@gmail.com",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
        };
        if (userManager.Users.All(u => u.Id != defaultUser.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "1q2w3E*");
                //await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
            }
        }
    }
    public static async Task SeedAdminUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        //Seed Default User
        var defaultUser = new IdentityUser
        {
            UserName = "admin@gmail.com",
            Email = "admin@gmail.com",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
        };
        if (userManager.Users.All(u => u.Id != defaultUser.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "1q2w3E*");
                await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
            }
        }
        await roleManager.SeedClaimsForAdmin();
    }

    public static async Task SeedSuperAdminUserAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        //Seed Admin User
        var defaultUser = new IdentityUser
        {
            UserName = "superadmin@gmail.com",
            Email = "superadmin@gmail.com",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true
        };
        if (userManager.Users.All(u => u.Id != defaultUser.Id))
        {
            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "1q2w3E*");
                await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
                await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
                await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
            }
        }
        await roleManager.SeedClaimsForSuperAdmin();
    }

    private async static Task SeedClaimsForSuperAdmin(this RoleManager<IdentityRole> roleManager)
    {
        var adminRole = await roleManager.FindByNameAsync("SuperAdmin");
        await roleManager.AddPermissionClaim(adminRole, "Products");
    }
    private async static Task SeedClaimsForAdmin(this RoleManager<IdentityRole> roleManager)
    {
        var adminRole = await roleManager.FindByNameAsync("Admin");
        await roleManager.AddPermissionClaim(adminRole, "Products");
    }

    public static async Task AddPermissionClaim(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
    {
        var allClaims = await roleManager.GetClaimsAsync(role);
        var allPermissions = Permissions.GeneratePermissionsForModule(module);
        foreach (var permission in allPermissions)
        {
            if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
            {
                await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
            }
        }
    }
}
