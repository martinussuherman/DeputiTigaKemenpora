using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Itm.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DeputiTigaKemenpora.Identity
{
    public static class UserRoleSetup
    {
        private const string UserRoleName = "User";

        public static async Task CreateUserRole(this IServiceProvider serviceProvider)
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            IServiceProvider services = scope.ServiceProvider;
            RoleManager<ApplicationRole> roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

            if (!await roleManager.RoleExistsAsync(UserRoleName))
            {
                await roleManager.CreateAsync(new ApplicationRole(UserRoleName));
            }

            ApplicationRole userRole = await roleManager.FindByNameAsync(UserRoleName);
            IList<Claim> claimList = await roleManager.GetClaimsAsync(userRole);

            if (claimList.Count == 0)
            {
                await roleManager.AddClaimAsync(userRole,
                    new Claim(Permissions.CustomClaimTypes, Permissions.Kegiatan.Create));
                await roleManager.AddClaimAsync(userRole,
                    new Claim(Permissions.CustomClaimTypes, Permissions.Kegiatan.Edit));
            }
        }
    }
}