using Microsoft.EntityFrameworkCore;
using Movies.BLL.Mapping;
using Movies.DAL.Data;
using Movies.WebAdmin.Helper.CookieExtensions;
using Movies.WebAdmin.Helper.Extensions;
using Movies.WebAdmin.Helper.IdentityExtensions;

namespace Movie.WEBUI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            //Mapping
            builder.Services.AddAutoMapper(typeof(CustomMapping));

            //Identity AppRole,AppUser Security 
            builder.Services.AddIdentityServices();

            //Generic Services Extensions
            builder.Services.AddServices();

            //Generic Repostory Extensions
            builder.Services.AddRepositories();

            //Cookie Service
            builder.Services.AddCookieServices();

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
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}