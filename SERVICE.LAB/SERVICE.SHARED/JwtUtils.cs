using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared;

namespace Shared
{
    public interface IJwtUtils
    {
        public string GenerateToken(User? user = null);
        public User? ValidateToken(string token);
    }
    public class JwtUtils : IJwtUtils
    {
        private readonly AppSettings _appSettings;
        public JwtUtils(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public string GenerateToken(User? user = null)
        {
            if (user == null) user = new User(true, true, 1, 1, 1, "BloodBankMasters", true);
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JWT.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("IsAdmin", user.IsAdmin.ToString()),
                    new Claim("IsSuperAdmin", user.IsSuperAdmin.ToString()),
                    new Claim("VenueNo", user.VenueNo.ToString()),
                    new Claim("VenueBranchNo", user.VenueBranchNo.ToString()),
                    new Claim("UserNo", user.UserNo.ToString()),
                    new Claim("UserName", user.UserName),
                    new Claim("IsProvisional", user.IsProvisional.ToString()),
                    new Claim(ClaimTypes.Role, "BloodBankMasters")
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public User? ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JWT.Key);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var isAdmin = jwtToken.Claims.First(x => x.Type == "IsAdmin").Value == "True" ? true : false;
                var isSuperAdmin = jwtToken.Claims.First(x => x.Type == "IsSuperAdmin").Value == "True" ? true : false;
                var isProvisional = jwtToken.Claims.First(x => x.Type == "IsProvisional").Value == "True" ? true : false;
                var venueNo = Int32.Parse(jwtToken.Claims.First(x => x.Type == "VenueNo").Value);
                var venueBranchNo = Int32.Parse(jwtToken.Claims.First(x => x.Type == "VenueBranchNo").Value);
                var userNo = Int32.Parse(jwtToken.Claims.First(x => x.Type == "UserNo").Value);
                var userName = jwtToken.Claims.First(x => x.Type == "UserName").Value;
                var roles = jwtToken.Claims.Where(x => x.Type == "role").Select(x => x.Value).ToList();
                // return user id from JWT token if validation successful
                return new User(isAdmin, isSuperAdmin, venueNo, venueBranchNo, userNo, userName, isProvisional, roles);
            }
            catch (Exception exp)
            {
                // return null if validation fails
                return null;
            }
        }
    }
}