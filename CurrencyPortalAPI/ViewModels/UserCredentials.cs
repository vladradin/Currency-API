using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyPortalAPI.ViewModels
{
	public class UserCredentials
	{
		[Required]
		[MinLength(Validation.UsernameLength, ErrorMessage = Validation.UsernameLengthError)]
		public string Username { get; set; }

		[Required]
		[MinLength(Validation.PasswordLength, ErrorMessage = Validation.PasswordLengthError)]
		public string Password { get; set; }
	}
}
