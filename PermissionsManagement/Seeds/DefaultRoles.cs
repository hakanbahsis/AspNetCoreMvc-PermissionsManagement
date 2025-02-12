﻿using Microsoft.AspNetCore.Identity;
using PermissionsManagement.Constants;

namespace PermissionsManagement.Seeds;

public static class DefaultRoles
{
    public static async Task SeedAsync(UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager)
    {
        await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
        await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));
    }
}
