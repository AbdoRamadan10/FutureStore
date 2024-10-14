using FutureStore.Models;
using Microsoft.EntityFrameworkCore;

namespace FutureStore.Data
{
    public class FutureStoreContext : DbContext
    {
        public FutureStoreContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

    }
}
