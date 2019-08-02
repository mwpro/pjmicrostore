using System.Security.Claims;

namespace Identity.Contracts
{
    public static class ClaimPrincipalExtensions
    {
        public static bool HasScope(this ClaimsPrincipal claimsPrincipal, string scopeName)
        {
            return claimsPrincipal.HasClaim("scope", scopeName);
        }
    }
}