using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyPortalAPI.ViewModels
{
	public class TokenViewModel
	{
		public TokenViewModel(string token = null)
		{
			Value = token;
		}

		public string Value { get; set; }
	}
}
