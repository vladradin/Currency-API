using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Models
{
	public class CurrencyConversion
	{
		public CurrencyConversion(CurrencySymbol fromCurrency, IEnumerable<Currency> rates, DateTime date)
		{
			From = fromCurrency;
			ToCurrencies = rates;
			OnDate = date;
		}

		public CurrencySymbol From { get; set; }

		public IEnumerable<Currency> ToCurrencies{get;set;}

		public DateTime OnDate { get; set; }
	}
}
