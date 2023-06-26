using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace JWTWebToken.Security
{
    public static class JwtTokenHandler
    {
        public static JwtTokenModel CreateToken(IConfiguration configuration)
        {
            JwtTokenModel token = new ();
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes
                (configuration["JwtTokenSetting:SecurityKey"]));

            SigningCredentials credentials = new SigningCredentials
                (securityKey, SecurityAlgorithms.HmacSha256);

            token.ExpiresTime = DateTime.Now.AddMinutes(Convert.ToInt16(configuration["JwtTokenSetting:Expiration"]));

            JwtSecurityToken jwtSecurityToken = new(
                    issuer: configuration["JwtTokenSetting:Issuer"],
                    audience: configuration["JwtTokenSetting:Audinence"],
                    expires: token.ExpiresTime,
                    notBefore: DateTime.Now,
                    signingCredentials: credentials
                );
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);

            byte[]numbers=new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(numbers);
            token.RefreshToken=Convert.ToBase64String(numbers);
            
            return token;
        }
    }
}
