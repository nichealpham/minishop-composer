using Microsoft.Extensions.Configuration;
using AppGlobal.Extensions;
using System;
using AppGlobal.Models;
using System.Collections.Generic;
using System.Linq;
using AppGlobal.Entities;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using AppGlobal.Constants;
using Newtonsoft.Json;

namespace AppGlobal.Services
{
    public class AccessTokenService
    {
        private readonly string _EncriptionKey;

        public AccessTokenService(IConfiguration Configuration)
        {
            _EncriptionKey = Configuration["AccessTokenEncriptionKey"];
        }

        public string GenerateJwtToken(AuthEntity auth)
        {
            var fullName = auth.FullName;
            var userID = auth.UserID;
            var email = auth.Email;
            var phone = auth.Phone;
            var rolesStr = auth.Roles.ToJsonString();

            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_EncriptionKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { 
                    new Claim("FullName", fullName),
                    new Claim("UserID", userID),
                    new Claim("Email", email),
                    new Claim("Phone", phone),
                    new Claim("Roles", rolesStr),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public AuthEntity ParseJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_EncriptionKey);
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero,
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            var fullName = jwtToken.Claims.First(x => x.Type == "FullName").Value;
            var userID = jwtToken.Claims.First(x => x.Type == "UserID").Value;
            var email = jwtToken.Claims.First(x => x.Type == "Email").Value;
            var phone = jwtToken.Claims.First(x => x.Type == "Phone").Value;
            var rolesStr = jwtToken.Claims.First(x => x.Type == "Roles").Value;
            var roles = rolesStr.ToJsonObject<List<UserRole>>();

            var authUser = new AuthEntity()
            {
                FullName = fullName,
                UserID = userID,
                Phone = phone,
                Roles = roles,
                Email = string.IsNullOrEmpty(email) ? null : email,
            };

            return authUser;
        }
    }
}
