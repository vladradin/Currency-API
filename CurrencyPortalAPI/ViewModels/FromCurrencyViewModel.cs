using Bussiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyPortalAPI.ViewModels
{
	public class CurrencyViewModel
	{
		public string Symbol { get; set; }
		public decimal Amount { get; set; }
	}
}
