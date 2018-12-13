using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityDemo.Authentication.JWT.AuthHelper
{
    public class ResourceAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, IResourceWithCreator>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, IResourceWithCreator resource)
        {
            //if (context.User.IsInRole("admin"))
            //{
            //    context.Succeed(requirement);
            //}

            if (requirement == ResourceOperations.Create || requirement == ResourceOperations.Read)
            {
                context.Succeed(requirement);
            }
            else
            {
                if (context.User.Identity.Name == resource.Creator)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}
