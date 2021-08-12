using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.Data.Models;
using WebAPI.Utility.Interfaces;

namespace WebAPI.Utility
{
    public class JwtHelper : IJwtHelper
    {
        private readonly AppSettings _appSettings;

        public JwtHelper(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secrate);

            var permissionsClaim = new List<Claim>();
            permissionsClaim.Add(new Claim(ClaimTypes.Name, user.Email));
            permissionsClaim.Add(new Claim(ClaimTypes.Role, user.role.RoleName));
            permissionsClaim.Add(new Claim(ClaimTypes.Role, user.role.RoleName));
            foreach (var item in user.role.Permissions)
            {
                permissionsClaim.Add(new Claim(ClaimTypes.Role, item.PermissionName));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(permissionsClaim),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
