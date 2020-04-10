using Microsoft.EntityFrameworkCore;
using shop_balta.Models;

namespace shop_balta
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> User { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
