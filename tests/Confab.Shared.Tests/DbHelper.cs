using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Confab.Shared.Tests
{
    public static class DbHelper
    {
        private static readonly IConfiguration _configuration = OptionsHelper.GetConfigurationRoot();

        public static DbContextOptions<T> GetOptions<T>() where T : DbContext
        {
            return new DbContextOptionsBuilder<T>()
                .UseNpgsql(_configuration["postgres:connectionString"])
                .EnableSensitiveDataLogging()
                .Options;
        }
    }
}