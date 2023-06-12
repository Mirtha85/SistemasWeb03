using Microsoft.EntityFrameworkCore;
using SistemasWeb01.Models;
using static System.Collections.Specialized.BitVector32;
using System.Drawing;

namespace SistemasWeb01.DataAccess
{
    public class ShoppingDbContext : DbContext
    {
        public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options) : base(options)
        {
            Database.Migrate();// this will migrate the database on startup = update-database
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Talla> Tallas { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
            modelBuilder.Entity<SubCategory>().HasIndex("Name", "CategoryId").IsUnique();

            modelBuilder.Entity<Talla>().HasIndex(p => p.ShortName).IsUnique();
            modelBuilder.Entity<Product>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<Product>().HasIndex("Name", "SubCategoryId").IsUnique();
            modelBuilder.Entity<ProductSize>().HasIndex("ProductId", "TallaId").IsUnique();

        }
    }
}
