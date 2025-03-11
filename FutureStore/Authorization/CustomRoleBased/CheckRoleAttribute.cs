//using FutureStore.Authorization.PermissionBased;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Claims;

//namespace FutureStore.Authorization.CustomRoleBased
//{
//    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
//    public class CheckRoleAttribute : Attribute, IAuthorizationFilter
//    {
//        private readonly Role _role;
//        private readonly Permission _permission;

//        public CheckRoleAttribute(Role role, Permission permission)
//        {
//            _role = role;
//            _permission = permission;
//        }

//        public void OnAuthorization(AuthorizationFilterContext context)
//        {
//            var claimIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
//            if (claimIdentity == null || !claimIdentity.IsAuthenticated)
//            {
//                context.Result = new ForbidResult();
//                return;
//            }

//            var userRole = Enum.Parse<Role>(claimIdentity.FindFirst(ClaimTypes.Role)?.Value);
//            if (userRole != _role || !RolePermissions.Permissions[userRole].Contains(_permission))
//            {
//                context.Result = new ForbidResult();
//            }
//        }
//    }
//}
