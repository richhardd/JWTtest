using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace webJWT
{
    public class TokenManager
    {
        private static string Secret = "XCAP05H6LoKvbRRa/QkqLNMI7cOHguaRyHzyg7n5qEkGjQmtBhz4SzYh4Fqwjyi3KJHlSXKPwVu2+bXr6CtpgQ==";

        public static string GenerateToken (string username)
        {
            byte[] Key = Convert.FromBase64String(Secret);
            SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Key);
            SecurityTokenDescriptor Descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler Handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = Handler.CreateJwtSecurityToken(Descriptor);
            return Handler.WriteToken(token);

        }
    }
}