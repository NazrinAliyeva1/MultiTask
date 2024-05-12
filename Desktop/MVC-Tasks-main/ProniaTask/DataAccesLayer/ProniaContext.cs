using Microsoft.EntityFrameworkCore;
using ProniaTask.Models;

namespace ProniaTask.DataAccesLayer
{
    public class ProniaContext : DbContext
    {
        public ProniaContext(DbContextOptions options) : base(options)
        {            

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Slider>Sliders { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=LAPTOP-PVUROI38\\SQLEXPRESS; Database=AB106Pronia; Trusted_Connection=True;TrustServerCertificate=True");
            base.OnConfiguring(options);
        }
        
    }
}
