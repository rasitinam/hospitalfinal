using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

public class AuthenticationService : hospitalfinal.Models.IAuthenticationService
{
    private readonly string _secretKey = "yourSecretKey";  // Güvenli bir şekilde saklayın
    private readonly string _issuer = "yourIssuer";
    private readonly string _audience = "yourAudience";

    // JWT token'ı oluşturma fonksiyonu
    public string GenerateJwtToken(string userId, string userName)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(ClaimTypes.Name, userName),
            // Diğer claim'ler eklenebilir
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.Now.AddDays(1), // Token'ın geçerlilik süresi
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
