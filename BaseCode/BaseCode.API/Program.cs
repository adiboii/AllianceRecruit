using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BaseCode.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .ConfigureAppConfiguration(SetUpConfiguration)
                .UseStartup<Startup>();

        private static void SetUpConfiguration(WebHostBuilderContext builderCtx, IConfigurationBuilder config)
        {
            config.Sources.Clear();     // Clears the default configuration options

            IHostingEnvironment env = builderCtx.HostingEnvironment;

            // Include settings file back to the configuration
            config.SetBasePath(env.ContentRootPath) 
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                   .AddEnvironmentVariables();
        }
    }
}
