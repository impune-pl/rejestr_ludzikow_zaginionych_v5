using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using rejestr_ludzikow_zaginionych_v5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rejestr_ludzikow_zaginionych_v5.Authorization
{
    public class PersonOwnerOrAdminAuthorizationHandler : AuthorizationHandler<SameOwnerRequirement, Person>
    {
        UserManager<User> _userManager;

        public PersonOwnerOrAdminAuthorizationHandler(UserManager<User>
            userManager)
        {
            _userManager = userManager;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, SameOwnerRequirement requirement, Person resource)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }
            else if (context.User.IsInRole(Constants.ADMINISTRATOR_ROLE))
            {
                context.Succeed(requirement);
            }
            else if(context.User.IsInRole(Constants.USER_ROLE))
            {
                if(resource.Owner.Id == int.Parse(_userManager.GetUserId(context.User)))
                {
                    context.Succeed(requirement);
                }
            }
            
            return Task.CompletedTask;
        }
    }
}
public class SameOwnerRequirement : IAuthorizationRequirement { }