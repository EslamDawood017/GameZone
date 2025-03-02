using GameZone.Models;
using Microsoft.AspNetCore.Identity;

namespace GameZone
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var ConnectionString = builder.Configuration.GetConnectionString("DefualtConnection") ??
                throw new InvalidOperationException("No Connection String Was Found");

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(ConnectionString));
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<ICategoriesService,CategoriesService>();
            builder.Services.AddScoped<IDevicesService,DevicesService>();
			builder.Services.AddScoped<IGameService,GameService>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
                option => option.Password.RequireUppercase = false)
                .AddEntityFrameworkStores<AppDbContext>();


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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
