//using FutureStore.Authorization.PermissionBased;
//using FutureStore.Data;
//using FutureStore.Models.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.EntityFrameworkCore;
//using System.Security.Claims;

//namespace FutureStore.Authorization.CustomRoleBased
//{
//    public class RoleBasedAuthorizationFilter(FutureStoreContext dbContext) : IAuthorizationFilter
//    {
//        public void OnAuthorization(AuthorizationFilterContext context)
//        {
//            //var attribute = context.ActionDescriptor.EndpointMetadata
//            //    .OfType<CheckPermissionAttribute>()
//            //    .FirstOrDefault();

//            var attribute = context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is CheckRoleAttribute) as CheckRoleAttribute;


//            if (attribute != null)
//            {


//                var claimIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
//                if (claimIdentity == null || !claimIdentity.IsAuthenticated)
//                {
//                    context.Result = new ForbidResult();
//                }
//                else
//                {


//                    var userId = int.Parse(claimIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value);
//                    var userRole = Enum.Parse<Role>(claimIdentity.FindFirst(ClaimTypes.Role)?.Value);
//                    var hasPermission = dbContext.Set<User>().Any(x => x.Id == userId );
//                    if (!hasPermission)
//                    {
//                        context.Result = new ForbidResult();
//                    }

//                }
//            }

//        }
//    }
//}
