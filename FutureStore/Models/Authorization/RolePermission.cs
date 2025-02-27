using FutureStore.Enums;

namespace FutureStore.Models.Authorization
{
    public class RolePermission
    {
        public RoleEnum RoleId { get; set; }

        public Role? Role { get; set; }

        public PermissionEnum PermissionId { get; set; }

        public Permission? Permission { get; set; }
    }
}
