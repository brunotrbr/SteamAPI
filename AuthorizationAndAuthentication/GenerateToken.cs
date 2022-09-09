using Microsoft.IdentityModel.Tokens;
using SteamAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SteamAPI.AuthorizationAndAuthentication
{
    public class GenerateToken
    {
        private readonly TokenConfiguration _configuration;
        public GenerateToken(TokenConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwt(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.Secret));
            var tokenHandler = new JwtSecurityTokenHandler();

            var nameClaim = new Claim(ClaimTypes.Name, user.Username);
            var roleClaim = new Claim(ClaimTypes.Role, user.Role);
            List<Claim> claims = new List<Claim>();
            claims.Add(nameClaim);
            claims.Add(roleClaim);

            var jwtToken = new JwtSecurityToken(
                issuer: _configuration.Issuer,
                audience: _configuration.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(_configuration.ExpirationtimeInHours),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature));

            return tokenHandler.WriteToken(jwtToken);
        }
    }
}
