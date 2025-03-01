using FutureStore.Models.Authorization;
using FutureStore.Models.Buisness;
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
        //public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<User>().ToTable("Users");
            //modelBuilder.Entity<UserPermission>().ToTable("UserPermissions").HasKey(x=> new {x.UserId,x.PermissionId });





            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Permission>().ToTable("Permissions");
            modelBuilder.Entity<RolePermission>().ToTable("RolePermissions").HasKey(x => new { x.RoleId, x.PermissionId });


        }

    }
}
