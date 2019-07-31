using System;
using Microsoft.AspNetCore.Authorization;

namespace Identity.Contracts
{
    public static class AuthorizationOptionsExtensions
    {
        public static AuthorizationOptions AddAdminOnlyPolicy(this AuthorizationOptions authorizationOptions)
        {
            authorizationOptions.AddPolicy(AuthorizationPolicies.AdminOnly,
                builder => builder.RequireRole(Roles.Admin).Build());
            return authorizationOptions;
        }
    }
}
