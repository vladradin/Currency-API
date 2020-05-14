using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace CurrencyPortalAPI.ErrorHandling
{
	public class InvalidIdentityDataFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			switch (context.Exception)
			{
				case UserIdentityException invalidUserData:
					HandleInvalidUserData(invalidUserData, context);
					break;
			}
		}

		private void HandleInvalidUserData(UserIdentityException invalidUserData, ExceptionContext context)
		{
			context.ExceptionHandled = true;
			context.Result = new JsonResult(GetErrorsByCode(invalidUserData.Errors));
			context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
		}

		IDictionary<string, IEnumerable<string>> GetErrorsByCode(IEnumerable<IdentityError> errors)
		{
			var dictionary = new Dictionary<string, IEnumerable<string>>();
			var groupsByName = errors.GroupBy(error => error.Code);
			foreach (var group in groupsByName)
			{
				dictionary.Add(
					group.Key,
					group.Select(error => error.Description).ToList()
				);
			}

			return dictionary;
		}
	}
}
