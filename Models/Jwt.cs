using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AirlineWebAPI.Models
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Duration { get; set; }
        public Jwt(string Key,string Duration) {
            this.Key = Key ?? "";
            this.Duration = Duration ?? "";
        }
        public string GenerateToken(RegisterUser user)
        {
            var key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Key));
             var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Userid",user.UserId.ToString()),
                 new Claim("Username",user.Username),
                  new Claim("Email",user.Email),
                   new Claim("Age",user.Age.ToString()),
                    new Claim("Password",user.Password),
                     new Claim("ConfirmPassword",user.ConfirmPassword),
                      new Claim("UserType",user.UserType),
            };
            var jwtToken = new JwtSecurityToken(issuer:"localhost", audience: "localhost", claims: claims,expires: DateTime.Now.AddMinutes(Int32.Parse(this.Duration)),signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

    }
}
