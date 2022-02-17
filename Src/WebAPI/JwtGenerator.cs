using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI
{
    public class JwtGenerator
    {
        public static string Generate(User user, IConfiguration configuration)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("Id", user.Id.ToString()));
            claims.Add(new Claim("Roletype", user.RoleType.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Key")));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
               signingCredentials: signingCredentials,
               claims: claims,
               expires: DateTime.Now.AddDays(1),
               audience: configuration.GetValue<string>("Jwt:Audience"),
               issuer: configuration.GetValue<string>("Jwt:Issuer")
               );
                 return tokenHandler.WriteToken(jwt);

        }


    }
}
