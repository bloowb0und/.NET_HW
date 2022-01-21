using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace KUnitFramework
{
    public class DependencyRegistrar_DAL
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDAL, DAL>();
        }
    }
}