using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using jwtAspNet.Models;
using Microsoft.IdentityModel.Tokens;

namespace jwtAspNet.Services;

public class TokenService
{
    public string Create(User user)
    {
        var handler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(Configuration.Key);

        var credentials = new SigningCredentials(
            key: new SymmetricSecurityKey(key),
            algorithm: SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(2),
            Subject = GenerateClaims(user)
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    public static ClaimsIdentity GenerateClaims(User user)
    {
        var claimsIdentity = new ClaimsIdentity();

        claimsIdentity.AddClaim(new Claim(type: "id", value: user.Id.ToString()));
        claimsIdentity.AddClaim(new Claim(type: ClaimTypes.Name, value: user.Email));
        claimsIdentity.AddClaim(new Claim(type: ClaimTypes.Email, value: user.Email));
        claimsIdentity.AddClaim(new Claim(type: ClaimTypes.GivenName, value: user.Name));
        claimsIdentity.AddClaim(new Claim(type: "image", value: user.Image));

        foreach (var role in user.Roles)
            claimsIdentity.AddClaim(new Claim(type: ClaimTypes.Role, value: role));

        return claimsIdentity;
    }
}