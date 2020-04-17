using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace webJWT.Models
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
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            JwtSecurityTokenHandler Handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = Handler.CreateJwtSecurityToken(Descriptor);
            return Handler.WriteToken(token);

        }
        public static ClaimsPrincipal GetPrincipal (string token)
        {
            try
            {
                JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken JwtToken = (JwtSecurityToken)TokenHandler.ReadToken(token);
                if (JwtToken == null) return null;
                byte[] key = Convert.FromBase64String(Secret);
                TokenValidationParameters parameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = TokenHandler.ValidateToken(token, parameters, out securityToken);
                return principal;
            }catch(Exception e)
            {
                return null;
            }
        }
        public static string ValidateToken (string token)
        {
            string username = null;
            ClaimsPrincipal principal = GetPrincipal(token);
            if (principal == null) return null;
            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException)
            {
                return null;
            }
            Claim usernameClaim = identity.FindFirst(ClaimTypes.Name);
            username = usernameClaim.Value;
            return username;
        }
    }
}