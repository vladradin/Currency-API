using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CurrencyPortalAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args)
		{
			return WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.ConfigureAppConfiguration((hostContext, config) =>
				{
					var env = hostContext.HostingEnvironment.EnvironmentName;

					// delete all default configuration providers
					config.Sources.Clear();
					config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
					config.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);
				}); ;
		}
	}
}
