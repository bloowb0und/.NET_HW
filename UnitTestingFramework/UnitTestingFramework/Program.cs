using System.IO;
using System.Reflection;
using KUnitFramework;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTestingFramework
{
    internal class Program
    {
        public static void Main()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<App>().StartApp();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<App>();
            DependencyRegistrar_BLL.ConfigureServices(services);
        }
    }
}