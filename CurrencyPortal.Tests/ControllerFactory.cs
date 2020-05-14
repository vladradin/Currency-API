using CurrencyPortalAPI.Controllers;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using UserManagement;

namespace CurrencyPortal.Tests
{
	public class ControllerFactory
	{
		private readonly IServiceProvider _serviceProvider;

		public ControllerFactory(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public UsersController CreateUsersController()
		{
			var userManager = GetUserManager();
			return new UsersController(userManager);
		}

		public AuthenticationController CreateAuthentcationController()
		{
			var userManager = GetUserManager();
			var jwtTokenService = GetTokenService();

			return new AuthenticationController(userManager, jwtTokenService);
		}

		public CurrencyController CreateCurrencyController()
		{
			var currencyService = GetCurrencyService();
			return new CurrencyController(currencyService);
		}


		UserManager<AppUser> GetUserManager()
		{ 
			var userManageObj = _serviceProvider.GetService(typeof(UserManager<AppUser>)) ;
			return userManageObj as UserManager<AppUser>;
		}

		IJwtTokenService GetTokenService()
			=> _serviceProvider.GetService(typeof(IJwtTokenService)) as IJwtTokenService;

		ICurrencyService GetCurrencyService()
			=> _serviceProvider.GetService(typeof(ICurrencyService)) as ICurrencyService;
	}
}
