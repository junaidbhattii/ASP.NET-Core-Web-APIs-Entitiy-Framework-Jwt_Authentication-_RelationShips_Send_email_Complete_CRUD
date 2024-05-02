using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthentication_Relations_Authorization.Utils
{
    public class JWT
    {
        private readonly IConfiguration _configuration;
        public JWT(IConfiguration iConfiguration)
        {
            _configuration = iConfiguration;
        }
        public string generateToken(string email)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, email)
                };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:JWT_Secret").Value));
            SigningCredentials signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: _configuration.GetSection("JWT:JWT_Issuer").Value,
                audience: _configuration.GetSection("JWT:JWT_Audence").Value,
                signingCredentials: signingCred
                );
            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}
