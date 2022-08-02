namespace web.Infrastructure;

using System.Security.Claims;

public static class IdentityExtensions
{
    public static string GetId(this ClaimsPrincipal user)
        => user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
}