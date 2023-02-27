using Serilog;
using Serilog.Events;

namespace Movies.WebAdmin.Helper.LogExtensions
{
    public static class ImportandLogExtend
    {
        public static IServiceCollection AddImpotandLogServices(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration().MinimumLevel.Error().WriteTo.Console()
                .WriteTo.File(@"Log/important-logs.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
         
            return services;
        }
    }
}
