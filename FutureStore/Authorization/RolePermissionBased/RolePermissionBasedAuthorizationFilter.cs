using FutureStore.Authorization.PermissionBased;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FutureStore.Data;
using FutureStore.Enums;
using FutureStore.Models.Authorization;

namespace FutureStore.Authorization.CustomRolePermissionBased
{
    public class RolePermissionBasedAuthorizationFilter(FutureStoreContext dbContext) : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //var attribute = context.ActionDescriptor.EndpointMetadata
            //    .OfType<CheckPermissionAttribute>()
            //    .FirstOrDefault();

            var attribute = (CheckRolePermissionAttribute)context.ActionDescriptor.EndpointMetadata.FirstOrDefault(x => x is CheckRolePermissionAttribute);


            if (attribute != null)
            {


                var claimIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
                if (claimIdentity == null || !claimIdentity.IsAuthenticated)
                {
                    context.Result = new ForbidResult();
                }
                else
                {


                    var userRoleString = claimIdentity.FindFirst(ClaimTypes.Role)?.Value; //This is string of userRole (ex Admin,User,Guest ......)
                    var userRole = (int)(RoleEnum)Enum.Parse(typeof(RoleEnum), userRoleString); //This is int of userRole (ex 1,2,3 ......)
                    var hasPermission = dbContext.Set<RolePermission>().Any(x => ((int)x.RoleId) == userRole && x.PermissionId == attribute.Permission);
                    if (!hasPermission)
                    {
                        context.Result = new ForbidResult();
                    }

                }
            }

        }
    }
}
