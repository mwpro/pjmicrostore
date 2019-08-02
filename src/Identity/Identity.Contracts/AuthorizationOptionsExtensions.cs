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

        public static AuthorizationOptions AddRequireScopePolicy(this AuthorizationOptions authorizationOptions, string scopeName)
        {
            authorizationOptions.AddPolicy(scopeName,
                builder => builder.RequireClaim("scope", scopeName).Build());
            return authorizationOptions;
        }
    }
}
