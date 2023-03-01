using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Movies.DAL.Data;
using Movies.BLL.Mapping;
using Movies.WebAdmin.Helper.Extensions;
using Movies.WebAdmin.Helper.FlluentExtensions;
using Serilog;
using Serilog.Events;
using Microsoft.Extensions.Logging;
using Movies.WebAdmin.Helper.LogExtensions;
using Movies.DAL.DbModel;
using Microsoft.AspNetCore.Identity;
using Movies.WebAdmin.Helper.IdentityExtensions;
using Movies.WebAdmin.Helper.CookieExtensions;

namespace Movies.WebAdmin
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
      

            // Add services to the container.
            builder.Services.AddControllersWithViews();/*.AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<Program>());*/

            //Fluent Validations Extensions
            builder.Services.AddFluentServices();
            
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            //Identity AppRole,AppUser Security 
            builder.Services.AddIdentityServices();

            //Cookie Service
            builder.Services.AddCookieServices();



            //builder.Services.Configure<IISServerOptions>(options =>
            //{
            //    options.MaxRequestBodySize = 838860800;
            //});

            //builder.Services.Configure<KestrelServerOptions>(options =>
            //{
            //    options.Limits.MaxRequestBodySize = 838860800;
            //});

            //All Logger Extensions
            builder.Services.AddAllLogServices();

            //Importand Logger Extensions
            //builder.Services.AddImpotandLogServices();

            //Mapping
            builder.Services.AddAutoMapper(typeof(CustomMapping));

            //Generic Services Extensions
            builder.Services.AddServices();

            //Generic Repostory Extensions
            builder.Services.AddRepositories();

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
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=LogIn}/{id?}");

            app.Run();

        }
    }
}