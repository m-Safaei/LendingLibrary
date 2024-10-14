using System.Security.Claims;

namespace LendingLibrary.Core.ExtensionMethods;

public static class UserExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        if(claimsPrincipal != null)
        {
            var data = claimsPrincipal.Claims.SingleOrDefault(c=>c.Type == ClaimTypes.NameIdentifier);
            if(data != null)
            {
                return Guid.Parse(data.Value);
            }
            return default;
        }
        return default;
    }
}
