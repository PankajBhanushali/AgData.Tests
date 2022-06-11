using Microsoft.Extensions.Configuration;

namespace AGDATA.ApiTests.Settings
{
    internal static class Configuration
    {
        private static readonly IConfiguration _configuration =
            new ConfigurationBuilder()
                    .AddEnvironmentVariables()
                    .AddJsonFile("appsettings.json")
#if DEBUG
                    .AddJsonFile("appsettings.Development.json")
#endif
                    .Build();
        public static string BaseUrl => _configuration["BaseUrl"];

    }
}
