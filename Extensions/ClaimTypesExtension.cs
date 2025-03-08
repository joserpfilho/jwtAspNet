using System.Security.Claims;

namespace jwtAspNet.obj.Extensions;

public static class ClaimTypesExtension
{
    public static int Id(this ClaimsPrincipal user)
    {
        try
        {
            return int.Parse(user.Claims.FirstOrDefault(c => c.Type == "id")?.Value ?? "0");
        }
        catch
        {
            return 0;
        }
    }

    public static string Name(this ClaimsPrincipal user)
    {
        try
        {
            return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

    public static string Email(this ClaimsPrincipal user)
    {
        try
        {
            return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

    public static string GivenName(this ClaimsPrincipal user)
    {
        try
        {
            return user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }

    public static string Image(this ClaimsPrincipal user)
    {
        try
        {
            return user.Claims.FirstOrDefault(c => c.Type == "image")?.Value ?? string.Empty;
        }
        catch
        {
            return string.Empty;
        }
    }
}