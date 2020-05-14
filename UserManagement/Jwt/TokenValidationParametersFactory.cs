using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserManagement.Jwt
{
	public static class TokenValidationParametersFactory
	{
		public static TokenValidationParameters Create(JwtConfig jwtConfig)
		{
			var TokenValidationParameters = new TokenValidationParameters
			{
				ValidIssuer = jwtConfig.Issuer,
				ValidAudience = jwtConfig.Issuer,

				IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtConfig.Key)),
				ClockSkew = TimeSpan.Zero,

				RequireSignedTokens = true,
				RequireExpirationTime = true,
				ValidateAudience = true,

				ValidateLifetime = true,
			};

			return TokenValidationParameters;
		}
	}
}
