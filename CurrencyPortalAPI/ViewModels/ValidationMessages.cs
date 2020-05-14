using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyPortalAPI.ViewModels
{
	public static class Validation
	{
		public const string UsernameLengthError = "The user length must be at least {1} characters";
		public const string PasswordLengthError = "The password must have a minimum of {1} characters";

		public const int UsernameLength = 6;
		public const int PasswordLength = UsernameLength;
	}
}
