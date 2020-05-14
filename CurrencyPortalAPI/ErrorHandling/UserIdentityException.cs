using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyPortalAPI.ErrorHandling
{
	public class UserIdentityException : SystemException
	{
		public IEnumerable<IdentityError> Errors { get; }
		public UserIdentityException(IEnumerable<IdentityError> errors)
			=> Errors = errors;
	}
}
