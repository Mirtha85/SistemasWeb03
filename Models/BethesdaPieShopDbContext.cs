using Microsoft.EntityFrameworkCore;

namespace SistemasWeb01.Models
{
    public class BethesdaPieShopDbContext : DbContext
    {
        public BethesdaPieShopDbContext(DbContextOptions<BethesdaPieShopDbContext> options) : base(options)
        {
            Database.Migrate();// this will migrate the database on startup

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Pie> Pies { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
    }
}