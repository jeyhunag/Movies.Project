using FluentValidation.AspNetCore;
using FluentValidation;
using Movies.BLL.Validations;
using Serilog;


namespace Movies.WebAdmin.Helper.LogExtensions
{
    public static class AllLogExtend
    {
        public static IServiceCollection AddAllLogServices(this IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(@"Log/all-daily.txt", rollingInterval: RollingInterval.Day)
               
                .CreateLogger();

            return services;
        }

    }
}
