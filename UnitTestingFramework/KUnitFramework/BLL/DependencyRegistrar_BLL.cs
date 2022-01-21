using Microsoft.Extensions.DependencyInjection;

namespace KUnitFramework
{
    public class DependencyRegistrar_BLL
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IBLL, BLL>();
            DependencyRegistrar_DAL.ConfigureServices(services);
        }
    }
}