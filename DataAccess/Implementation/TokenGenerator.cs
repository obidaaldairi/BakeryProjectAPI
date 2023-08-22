using Domin.Entity;
using Domin.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DataAccess.Implementation
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _config;
        public TokenGenerator(IConfiguration config)
        {
            _config = config;

        }
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                 new Claim("Id",user.ID.ToString()),
                 new Claim(JwtRegisteredClaimNames.Email,user.Email),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("secret_Key").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokenDescriptor);
            return tokenhandler.WriteToken(token);
        }


    }
}
