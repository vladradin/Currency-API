using Bussiness.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Implementations;
using Repositories.Implementations.MySql;
using Repositories.Interfaces;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using UserManagement;
using UserManagement.Jwt;

namespace CurrencyPortalAPI
{
	public static class SetupDIExtensions
	{
		public static void ConfigureBussinesServices(this IServiceCollection services,CurrencyClientConfig clientConfig)
		{
			services.AddSingleton(clientConfig);

			services.AddScoped<ICurrencyService, CurrencyService>();
			services.AddScoped<ICurrencyClient, CurrencyClient>();
		}

		public static void ConfigureUserAuthentication (this IServiceCollection services, JwtConfig jwtConfig)
		{
			services.AddSingleton(jwtConfig);


			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
					.AddJwtBearer(ConfigureJwtOptions(jwtConfig));

			services.AddIdentityCore<AppUser>(opt =>
				opt.ConfigureClaimsOptions()
				   .ConfigurePasswordOptions()
			).AddDefaultTokenProviders();

			services.AddScoped<IJwtTokenService, JwtTokenService>();
			services.AddScoped<IUserStore<AppUser>, AppUserStore>();
		}

		public static Action<JwtBearerOptions> ConfigureJwtOptions(JwtConfig jwtConfig)
		{
			return bearerOptions =>
			{
				JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

				bearerOptions.RequireHttpsMetadata = false;

				bearerOptions.SaveToken = true;

				bearerOptions.TokenValidationParameters = TokenValidationParametersFactory.Create(jwtConfig);
			};
		}

		public static IdentityOptions ConfigurePasswordOptions(this IdentityOptions identityOpts)
		{			
			identityOpts.Password = new PasswordOptions
			{
				RequiredLength = 5,
				RequireDigit = true,
				RequireLowercase = false,
				RequireNonAlphanumeric = false,
				RequiredUniqueChars = 0,
				RequireUppercase = false
			};
			return identityOpts;
		}

		public static IdentityOptions ConfigureClaimsOptions(this IdentityOptions identityOpts)
		{
			identityOpts.ClaimsIdentity.UserNameClaimType = AppClaims.Username;
			identityOpts.ClaimsIdentity.UserIdClaimType = AppClaims.UserId;

			return identityOpts;
		}

		public static void ConfigureReposDI(this IServiceCollection services, ConnectionSettings connectionSettings)
		{
			services.AddSingleton(connectionSettings);

			services.AddScoped<IDatabaseFactory, MySqlDatabaseFactory>();
			services.AddScoped<IUserRepository, UsersRepository>();
			services.AddScoped<ICurrencyRepository, CurrencyRepository>();
		}
	}
}
