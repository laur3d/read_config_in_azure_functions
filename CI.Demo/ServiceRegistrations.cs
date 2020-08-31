using CI.Demo.FeatureFolder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CI.Demo
{
    internal static class ServiceRegistrations
    {
        public static void ConfigureServices(IServiceCollection builderServices, IConfiguration configuration)
        {
            builderServices.AddFeature();
        }

        private static IServiceCollection AddFeature(this IServiceCollection serviceCollection)
        {
            // here we would group all feature related DI code
            serviceCollection.TryAddSingleton<FeatureConfig>(sp =>
            {
                var config = sp.GetService<IConfiguration>();

                return new FeatureConfig
                {
                    ConfigValue = config.GetValue<string>("ConfigValue"),
                    FeatureFlag = config.GetValue<bool>("FeatureFlag" )
                };
            });

            return serviceCollection;
        }
    }
}
