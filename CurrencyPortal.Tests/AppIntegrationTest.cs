using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace CurrencyPortal.Tests
{
	public class IntegrationTest
	{
		public TestServer Server { get; }
		public IServiceProvider ServiceProvider { get; }

		protected ControllerFactory _controllerFactory;

		public IntegrationTest()
		{
			Server = new TestServer(new WebHostBuilder()
				.UseStartup<TestStartup>()
				.UseConfiguration(new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build()
			));

			ServiceProvider = Server.Host.Services;

			_controllerFactory = new ControllerFactory(ServiceProvider);
		}
	}
}
