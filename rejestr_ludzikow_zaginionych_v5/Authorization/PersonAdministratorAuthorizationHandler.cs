using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using rejestr_ludzikow_zaginionych_v5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rejestr_ludzikow_zaginionych_v5.Authorization
{
    public class PersonAdministratorAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Person>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Person resource)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }
            if (context.User.IsInRole(Constants.ADMINISTRATOR_ROLE))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}