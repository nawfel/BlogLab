using BlogLab.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogLab.Services
{
    public class TokenService :ITokenService
    {

        private readonly SymmetricSecurityKey _Key;
        private readonly string _issuer;
        public TokenService(IConfiguration configuration)
        {
            _Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            _issuer = configuration["Jwt:Issuer"];
        }

        public string CreateToken(ApplicationUserIdentity user)
        {
            var claims = new List<Claim>
           {
               new Claim(JwtRegisteredClaimNames.NameId,user.ApplicationUserId.ToString()),
               new Claim(JwtRegisteredClaimNames.Email,user.Email)
           };
            var creds = new SigningCredentials(_Key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                _issuer,
                _issuer,
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
