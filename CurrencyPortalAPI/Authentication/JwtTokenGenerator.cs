using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyPortalAPI.Authentication
{
	public class JwtTokenGenerator
	{
		public readonly string _encryptionKey = string.Empty;
		public readonly int _tokenExpiration = 60;
		public string CreateToken()
		{
			var key = CreateSymetricSecurityKey();

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var expires = DateTime.Now.AddMinutes(_tokenExpiration);

			var token = new JwtSecurityToken("", "",
											  null, expires: expires, signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		private SymmetricSecurityKey CreateSymetricSecurityKey()
		  => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_encryptionKey));
	}
}
