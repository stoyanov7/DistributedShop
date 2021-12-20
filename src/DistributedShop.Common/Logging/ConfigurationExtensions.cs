namespace DistributedShop.Common.Logging
{
    using DistributedShop.Common.Logging.Settings;
    using DistributedShop.Common.Logging.Settings.Abstraction;
    using DistributedShop.Common.Logging.Settings.Settings;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;
    using Serilog;
    using Serilog.Events;
    using System;

    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection AddLogger(this IServiceCollection services)
            => services.AddTransient<IDistributedShopLogger, DistributedShopLogger>();

        public static IWebHostBuilder UseLogging(this IWebHostBuilder webHostBuilder)
            => webHostBuilder.UseSerilog((context, loggerConfiguration) =>
            {
                var serilogSettings = context
                    .Configuration
                    .GetOptions<SerilogSettings>();

                var seqSettings = context
                    .Configuration
                    .GetOptions<SeqSettings>();

                var appSettings = context
                    .Configuration
                    .GetOptions<AppSettings>();

                if (!Enum.TryParse<LogEventLevel>(serilogSettings.Level, true, out var level))
                    level = LogEventLevel.Information;

                var applicationName = string.Empty;

                if (appSettings != null && string.IsNullOrWhiteSpace(appSettings.Name))
                    applicationName = appSettings.Name;

                loggerConfiguration
                    .Enrich.FromLogContext()
                    .MinimumLevel.Is(level)
                    .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                    .Enrich.WithProperty("ApplicationName", applicationName);

                if (serilogSettings != null && serilogSettings.ConsoleEnabled)
                    loggerConfiguration.WriteTo.Console();

                if (seqSettings != null && seqSettings.Enabled)
                    loggerConfiguration.WriteTo.Seq(seqSettings.Url, apiKey: seqSettings.Token);
            });
    }
}
