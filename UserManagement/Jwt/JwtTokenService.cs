using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UserManagement
{
	public class JwtTokenService : IJwtTokenService
	{
		private readonly JwtConfig _jwtConfig;

		public JwtTokenService(JwtConfig jwtConfig)
		{
			_jwtConfig = jwtConfig;
		}

		public string GenerateToken(AppUser appUser)
		{
			var userClaims = new List<Claim>
			{
				new Claim(AppClaims.Username,appUser.UserName)
			};

			return GenerateJSONWebToken(userClaims);
		}

		private string GenerateJSONWebToken(IEnumerable<Claim> userClaims)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key));

			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(_jwtConfig.Issuer,
			  _jwtConfig.Issuer,
			  userClaims,
			  expires: DateTime.Now.AddMinutes(_jwtConfig.ExpiresInMinutes),
			  signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
