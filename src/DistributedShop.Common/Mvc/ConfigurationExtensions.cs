namespace DistributedShop.Common.Mvc
{
    using DistributedShop.Common.Types;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection AddInitializers(this IServiceCollection services, params Type[] initializers)
            => initializers == null
                ? services
                : services.AddTransient<IStartupInitializer, StartupInitializer>(c =>
                {
                    var startupInitializer = new StartupInitializer();
                    var validInitializers = initializers.Where(t => typeof(IInitializer).IsAssignableFrom(t));

                    foreach (var initializer in validInitializers)
                    {
                        startupInitializer.AddInitializer(c.GetService(initializer) as IInitializer);
                    }

                    return startupInitializer;
                });
    }
}
