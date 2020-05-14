using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyPortalAPI.ErrorHandling;
using CurrencyPortalAPI.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagement;

namespace CurrencyPortalAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IJwtTokenService _tokenService;

		public AuthenticationController(UserManager<AppUser> userManager, IJwtTokenService tokenService)
		{
			_userManager = userManager;
			_tokenService = tokenService;
		}

		[HttpPost("signin")]
		public async Task<TokenViewModel> Login(UserCredentials credentials)
		{
			var appUser = await _userManager.FindByNameAsync(credentials.Username);

			var isPasswordCorrect = await _userManager.CheckPasswordAsync(appUser, credentials.Password);

			if (isPasswordCorrect)
			{
				var token = _tokenService.GenerateToken(appUser);
				return new TokenViewModel(token);
			}

			throw InvalidCredentialsExceptions();
		}

		UserIdentityException InvalidCredentialsExceptions()
			=> new UserIdentityException(new[] 
			{
				new IdentityError
				{
					Code ="Credentials",
					Description ="Username or password is invalid"
				}
			});
	}
}