namespace OnlineSlotReports.Data.Seeding
{
    using Microsoft.Extensions.Configuration;
    using SimpleApp.Data;
    using System;
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider, IConfiguration configuration);
    }
}
