using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PollAPICorrect.Helpers;
using PollAPICorrect.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PollAPICorrect.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly UserContext _userContext;

        public UserService(IOptions<AppSettings> appSettings, UserContext userContext)
        {
            _appSettings = appSettings.Value;
            _userContext = userContext;
        }

        public User Authenticate(string username, string password)
        {
            var user = _userContext.Users.SingleOrDefault(x => x.UserName == username && x.Password == password);

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim("UserID", user.UserID.ToString()),
                    new Claim("Email", user.Email),
                    new Claim("UserName", user.UserName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            user.Password = null;

            return user;
        }
    }
}
