using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System.Text;

namespace NInfra.Common.Logging
{
    public static class SerilogHostingExtensions
    {
        public static IHostBuilder UseSerilogDefault(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((context, configuration) =>
            {
                var config = configuration
                    .ReadFrom.Configuration(context.Configuration)
                    .Enrich.FromLogContext();

#if DEBUG
                config.MinimumLevel.Debug()
#else
                config.MinimumLevel.Information()
#endif
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
                    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
                    .WriteTo.File("logs/application.log", LogEventLevel.Information, rollingInterval: RollingInterval.Day, retainedFileCountLimit: null, encoding: Encoding.UTF8);
            });

            return hostBuilder;
        }
    }
}
