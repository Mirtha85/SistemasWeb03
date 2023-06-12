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



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();

        }
    }
}
