using Bussiness.Models;
using Microsoft.Extensions.Configuration;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement;

namespace CurrencyPortalAPI
{
	public static class ConfigDIExtensions
	{
		public static AppConfiguration GetAppConfig(this IConfiguration configuration)
			=> new AppConfiguration
			{
				JwtSettings = configuration.GetSettings<JwtConfig>("JwtSettings"),
				ClientConfig = configuration.GetSettings<CurrencyClientConfig>("CurrencyClient"),
				ConnectionSettings = configuration.GetSettings<ConnectionSettings>("ConnectionSettings")
			};


		public static T GetSettings<T>(this IConfiguration configuration, string sectionName)
			where T : new()
		{
			var config = new T();

			configuration.GetSection(sectionName).Bind(config);

			return config;
		}

	}
}
