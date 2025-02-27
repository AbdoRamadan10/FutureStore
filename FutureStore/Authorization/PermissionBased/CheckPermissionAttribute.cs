﻿namespace FutureStore.Authorization.PermissionBased
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CheckPermissionAttribute : Attribute
    {
        public CheckPermissionAttribute(Permission permission)
        {
            Permission = permission;
        }

        public Permission Permission { get; }
    }
}
