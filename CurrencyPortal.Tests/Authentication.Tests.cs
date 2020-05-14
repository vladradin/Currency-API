using CurrencyPortalAPI.ErrorHandling;
using CurrencyPortalAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CurrencyPortal.Tests
{
	public class AuthenticationTests : IntegrationTest
	{
		[Fact]
		public async Task WithCorrectCredentialsShouldReturnToken()
		{
			var (validCredentials, _, registrationInfo) = GetTestUserData();

			await RegisterUser(registrationInfo);

			var authenticationController = _controllerFactory.CreateAuthentcationController();			

			var token = await authenticationController.Login(validCredentials);

			Assert.NotNull(token?.Value);
		}

		[Fact]
		public async Task WithIncorrectCredentialsShouldThrowInvalidIdentity()
		{
			var (_, invalidCredentials, registrationInfo) = GetTestUserData();

			await RegisterUser(registrationInfo);

			var authenticationController = _controllerFactory.CreateAuthentcationController();

			await Assert.ThrowsAsync<UserIdentityException>(
				async () => await authenticationController.Login(invalidCredentials));
		}

		private async Task RegisterUser(UserRegistrationInfo registrationInfo)
		{
			var usersController = _controllerFactory.CreateUsersController();
			await usersController.RegisterUser(registrationInfo);
		}

		private (UserCredentials validCredentials, UserCredentials invalidCredentials, UserRegistrationInfo registrationInfo) GetTestUserData()
		{
			var correctUsername = "vlad.alexandru";
			var correctPassword = "password1";
			var incorrectPassword = "password";

			var validCredentials = new UserCredentials
			{
				Username = correctUsername,
				Password = correctPassword,
			};

			var invalidCredentials = new UserCredentials
			{
				Username = correctUsername,
				Password = incorrectPassword,
			};

			var registrationInfo = new UserRegistrationInfo
			{
				Username = correctUsername,
				Password = correctPassword,
				ConfirmedPassword = correctPassword,
				Email = "vlad@own.email"
			};

			return (validCredentials, invalidCredentials, registrationInfo);
		}
	}
}
