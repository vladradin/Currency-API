using System;
using CurrencyPortal.Tests.Mockups;
using CurrencyPortalAPI;
using CurrencyPortalAPI.ErrorHandling;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Implementations;
using Repositories.Interfaces;
using Services.Interfaces;

namespace CurrencyPortal.Tests
{
	public class TestStartup
	{
		public TestStartup(IConfiguration configuration)
		{
			Configuration = configuration;
			sutStartup = new Startup(Configuration);
		}

		public IConfiguration Configuration { get; }

		private readonly Startup sutStartup;

		
		public void ConfigureServices(IServiceCollection services)
		{
			sutStartup.ConfigureServices(services);

			AddMockCurrencyClientService(services);

			AddRepositoryMockupServices(services);
		}

		private void AddMockCurrencyClientService(IServiceCollection services)
			=> services.AddSingleton<ICurrencyClient, MockCurrencyClient>();

		private void AddRepositoryMockupServices(IServiceCollection services)
		{ 
			services.AddSingleton<IUserRepository, InMemoryUserRepository>();
			services.AddSingleton<ICurrencyRepository, MockCurrencyRepository>();
		}
		
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
			=>	sutStartup.Configure(app, env);		
	}
}