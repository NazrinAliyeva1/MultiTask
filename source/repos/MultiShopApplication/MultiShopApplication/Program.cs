using MultiShopApplication.DataAccesLayer;
using Microsoft.EntityFrameworkCore;

namespace MultiShopApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<MultiShopDbContext>();
            var app = builder.Build();
            app.UseStaticFiles();
            app.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{Id?}");
            app.Run();

        }

    }
}

