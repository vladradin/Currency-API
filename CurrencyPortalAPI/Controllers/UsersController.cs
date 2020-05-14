using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using CurrencyPortalAPI.ErrorHandling;
using CurrencyPortalAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserManagement;

namespace CurrencyPortalAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly UserManager<AppUser> _userManager;

		public UsersController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}
		[HttpPost("register")]
		public async Task<string> RegisterUser([FromBody]UserRegistrationInfo userInfo)
		{
			var result = await _userManager.CreateAsync(
				new AppUser { UserName = userInfo.Username, Email = userInfo.Email },
				userInfo.Password
				);

			if (result.Succeeded)
				return userInfo.Username;

			throw new UserIdentityException(result.Errors);
		}
	}
}