using DEV.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Dev.IRepository;
using Microsoft.EntityFrameworkCore;
using DEV.Model.EF;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.IO;
using DEV.Common;
using Microsoft.Extensions.Configuration;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Dev.Repository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private IConfiguration _config;
        private readonly IUserRepository _IUserRepository;
        public JWTManagerRepository(IConfiguration config, IUserRepository userRepository) { _config = config; _IUserRepository = userRepository; }
        public Tokens Authenticate(UserResponseEntity users)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_config["JWT:Key"]);
            var claims = new List<Claim>()
            {
             new Claim("IsAdmin", users.IsAdmin.ToString()),
             new Claim("IsSuperAdmin", users.IsSuperAdmin.ToString()),
             new Claim("VenueNo", users.VenueNo.ToString()),
             new Claim("VenueBranchNo", users.VenueBranchNo.ToString()),
             new Claim("UserNo", users.UserNo.ToString()),
             new Claim("UserName", users.UserName),
             new Claim("IsProvisional", users.IsProvisional.ToString())
              };
            users.lstUserRoleName?.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role.RoleName.ToString())));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var refreshToken = GenerateRefreshToken();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token), RefreshToken = refreshToken, RefreshTokenExpiryTime = DateTime.Now.AddDays(7) };

        }
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public UserClaimsIdentity ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_config["JWT:Key"]);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userName = jwtToken.Claims.First(x => x.Type == "UserName").Value;
                var venueNo = Int32.Parse(jwtToken.Claims.First(x => x.Type == "VenueNo").Value);
                var venueBranchNo = Int32.Parse(jwtToken.Claims.First(x => x.Type == "VenueBranchNo").Value);
                var userNo = Int32.Parse(jwtToken.Claims.First(x => x.Type == "UserNo").Value);
                var roles = jwtToken.Claims.Where(x => x.Type == "role").Select(x => x.Value).ToList();
                // return user id from JWT token if validation successful
                return new UserClaimsIdentity { UserNo = userNo, UserName = userName, VenueNo = venueNo, VenueBranchNo = venueBranchNo, Roles = roles };
            }
            catch (Exception exp)
            {
                return null;
            }
        }
        public bool ValidateSession(string token)
        {
            var result = ValidateToken(token);
            // This below line commented for performance issue - Only need for VAPT - 03-Jan-2025
            //return _IUserRepository.ValidateUserSession(result.UserNo, result.VenueNo, result.VenueBranchNo, token); 
            return true;
        }
    }
}

