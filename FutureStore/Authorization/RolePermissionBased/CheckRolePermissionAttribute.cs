using FutureStore.Enums;

namespace FutureStore.Authorization.CustomRolePermissionBased
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CheckRolePermissionAttribute : Attribute
    {
        public CheckRolePermissionAttribute(PermissionEnum permission)
        {
            Permission = permission;
        }

        public PermissionEnum Permission { get; }
    }
}
