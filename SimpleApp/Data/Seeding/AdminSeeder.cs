namespace OnlineSlotReports.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using SimpleApp.Data;
    using SimpleApp.Data.Seeding;

    internal class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var admin = await userManager.FindByEmailAsync(configuration["Admin:UserName"]);

            if (admin == null)
            {
                IdentityResult result = await userManager.CreateAsync(
                   new IdentityUser
                   {
                       UserName = configuration["Admin:Username"],
                       Email = configuration["Admin:Email"],
                       EmailConfirmed = true,
                   }, configuration["Admin:Password"]);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }

            var user = await userManager.FindByEmailAsync(configuration["Admin:UserName"]);

            await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
        }
    }
}
