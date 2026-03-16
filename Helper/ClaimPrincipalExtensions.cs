using System.Security.Claims;

namespace Ecommerce.Helper
{
    public static class ClaimPrincipalExtensions
    {

        public static string GetUserId(this ClaimsPrincipal principal)
        {

            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

    }
}
