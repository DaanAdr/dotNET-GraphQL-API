using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using graphql_api.DataModels;
using Microsoft.IdentityModel.Tokens;

namespace graphql_api.Helperclasses
{
    public sealed class AuthLogic(IConfiguration configuration)
    {
        public string GetAuthToken(User user)
        {
            // Create the signing key
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["TokenSettings:Key"]));

            // Define the token expiration time
            DateTime expiration = DateTime.UtcNow.AddHours(1); // Token valid for 1 hour

            // Create claims for the token
            Claim[] claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Create the token
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: configuration["TokenSettings:Issuer"],
                audience: configuration["TokenSettings:Issuer"],
                claims: claims,
                expires: expiration,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            // Return the token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
