using CurrencyPortalAPI.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyPortalAPI.ViewModels
{
	[HaveSameValue(nameof(Password),nameof(ConfirmedPassword),ErrorMessage ="{0} is not equal to {1} in value")]
	public class UserRegistrationInfo
	{
		[Required]
		[MinLength(Validation.UsernameLength, ErrorMessage = Validation.UsernameLengthError)]
		public string Username { get; set; }

		[Required]
		[MinLength(Validation.PasswordLength, ErrorMessage = Validation.PasswordLengthError)]
		public string Password { get; set; }

		[Required]
		[MinLength(Validation.PasswordLength, ErrorMessage = Validation.PasswordLengthError)]
		public string ConfirmedPassword { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}
