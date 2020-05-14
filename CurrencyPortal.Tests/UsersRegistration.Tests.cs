using CurrencyPortalAPI.ErrorHandling;
using CurrencyPortalAPI.ViewModels;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UserManagement;
using Xunit;

namespace CurrencyPortal.Tests
{
	public class UsersRegistrationTests : IntegrationTest
	{
		[Fact]
		public async Task ShouldReturnSuccessOnValidUserRegistration()
		{
			var usersController = _controllerFactory.CreateUsersController();


			const string password = "password1";
			var registrationModel = new UserRegistrationInfo
			{
				Username = "vlad.alexandru",
				Password = password,
				ConfirmedPassword = password,
				Email = "vlad@own.email"
			};

			var username = await usersController.RegisterUser(registrationModel);
			
			
			Assert.Equal(registrationModel.Username, username);
		}

		[Fact]
		public async Task ShouldRegistrationFaildOnPasswordWithoutDigit()
		{
			var usersController = _controllerFactory.CreateUsersController();

			const string password = "password";
			var registrationModel = new UserRegistrationInfo
			{
				Username = "vla",
				Password = password,
				ConfirmedPassword = password,
				Email = "vlad@own.email"
			};

			await Assert.ThrowsAsync<UserIdentityException>(
				async () => await usersController.RegisterUser(registrationModel));
		}
	}
}
