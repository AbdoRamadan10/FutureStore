using FutureStore.Models.Audit;
using FutureStore.Models.Authorization;
using FutureStore.Models.Buisness;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;
using System.Text;

namespace FutureStore.Data
{
    public class FutureStoreContext : DbContext
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public FutureStoreContext(DbContextOptions dbContextOptions, IHttpContextAccessor contextAccessor)
        : base(dbContextOptions)
        {
            _contextAccessor = contextAccessor;
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }
        //public DbSet<User> Users { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifiedEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted);

            var auditLogs = new List<AuditLog>();

            foreach (var modifiedEntity in modifiedEntities)
            {
                var auditLog = new AuditLog
                {
                    EntityName = modifiedEntity.Entity.GetType().Name,
                    UserId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                    UserName = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name)?.Value,
                    Action = modifiedEntity.State.ToString(),
                    Timestamp = DateTime.Now,
                    Changes = GetChanges(modifiedEntity)
                };
                auditLogs.Add(auditLog);
            }

            AuditLogs.AddRange(auditLogs);
            return base.SaveChangesAsync(cancellationToken);
        }



        private string GetChanges(EntityEntry modifiedEntity)
        {
            var changes = new StringBuilder();
            foreach (var property in modifiedEntity.OriginalValues.Properties)
            {
                var originalValue = modifiedEntity.OriginalValues[property];
                var currentValue = modifiedEntity.CurrentValues[property];
                if (!Equals(originalValue,currentValue))
                {
                    changes.AppendLine($"{property.Name} : Form '{originalValue}' to '{currentValue}'");
                }
            }

            return changes.ToString();


        }

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
