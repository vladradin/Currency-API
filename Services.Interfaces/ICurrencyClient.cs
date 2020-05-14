using Bussiness.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interfaces
{
	public interface ICurrencyClient
	{
		Task<CurrencyConversion> GetCurrenciesFor(CurrencySymbol fromCurrency);
		Task<IEnumerable<CurrencySymbol>> GetCurrencies();
	}
}
