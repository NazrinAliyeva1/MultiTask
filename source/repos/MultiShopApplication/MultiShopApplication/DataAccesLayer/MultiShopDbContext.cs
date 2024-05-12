using Microsoft.EntityFrameworkCore;
using MultiShopApplication.Models;


namespace MultiShopApplication.DataAccesLayer
{
    public class MultiShopDbContext : DbContext
    {
        public MultiShopDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=LAPTOP-PVUROI38\\SQLEXPRESS; Database=AB106MultiTask; Trusted_Connection= True;TrustServerCertificate=True"); 
            base.OnConfiguring(options);
        }

        

    }
}
