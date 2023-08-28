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
        private readonly IUnitOfWork _unitOfWork;
        public TokenGenerator(IConfiguration config, IUnitOfWork unitOfWork)
        {
            _config = config;
            _unitOfWork = unitOfWork;
        }
        public string CreateToken(User user)
        {

            //var roleuser = _unitOfWork.UserRole.FindByConditionWithIncludes(q => q.UsersId == user.ID, u => u.Role).Role.EnglishRoleName;
            var roleuser = _unitOfWork.UserRole.GetUserRole(user.ID);
            var claims = new List<Claim>
            {
                 new Claim("Id",user.ID.ToString()),
                 new Claim(ClaimTypes.Role, roleuser),
                 new Claim("Email",user.Email),
                 //new Claim("Role",roleuser),


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
