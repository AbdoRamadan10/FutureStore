using FutureStore.Enums;

namespace FutureStore.Models.Authorization
{
    public class Permission
    {
        public PermissionEnum PermissionId { get; set; }
        public string Code { get; set; }

        public string? Description { get; set; }
    }
}
