using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Movies.DAL.Data;
using Movies.BLL.Mapping;
using Movies.WebAdmin.Helper.Extensions;
using Movies.WebAdmin.Helper.FlluentExtensions;
using Serilog;
using Serilog.Events;

namespace Movies.WebAdmin
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
      

            // Add services to the container.
            builder.Services.AddControllersWithViews();/*.AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<Program>());*/

            builder.Services.AddFluentServices();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            //builder.Services.Configure<IISServerOptions>(options =>
            //{
            //    options.MaxRequestBodySize = 838860800;
            //});

            //builder.Services.Configure<KestrelServerOptions>(options =>
            //{
            //    options.Limits.MaxRequestBodySize = 838860800;
            //});
            builder.Services.AddAutoMapper(typeof(CustomMapping));
            
            builder.Services.AddServices();
            builder.Services.AddRepositories();

            var _logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo.Console()
                .WriteTo.File(@"Log/all-daily.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            builder.Logging.AddSerilog(_logger);

            var _loggerImportant = new LoggerConfiguration().MinimumLevel.Error().WriteTo.Console()
                .WriteTo.File(@"Log/important-logs.txt", restrictedToMinimumLevel: LogEventLevel.Warning)
                .CreateLogger();
            builder.Logging.AddSerilog(_loggerImportant);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }



            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}