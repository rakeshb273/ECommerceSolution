using Microsoft.EntityFrameworkCore;
using PhoneShopShareLibrary.Models;

namespace PhoneShopServer.Data
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {
            
        }
        public DbSet<Product> Products { get; set; } = default!;
    }
}
