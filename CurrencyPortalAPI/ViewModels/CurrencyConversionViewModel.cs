using Bussiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyPortalAPI.ViewModels
{
	public class CurrencyConversionViewModel
	{

		public CurrencyViewModel FromCurrency { get; set; }
		public DateTime Date { get; set; }
		public IEnumerable<CurrencyViewModel> Rates { get; set; }

		public CurrencyConversionViewModel(CurrencyConversion currencyConversion, decimal amount)
		{
			FromCurrency = new CurrencyViewModel
			{
				Symbol = currencyConversion.From.ToString(),
				Amount = amount
			};

			Date = currencyConversion.OnDate;

			Rates = currencyConversion
						.ToCurrencies
						.Select(ToCurrencyViewModel);
		}


		private CurrencyViewModel ToCurrencyViewModel(Currency currency)
			=> new CurrencyViewModel
			{
				Symbol = currency.Symbol.ToString(),
				Amount = currency.Amount
			};
	}
}
