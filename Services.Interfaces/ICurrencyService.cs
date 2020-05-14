using Bussiness.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
	public interface ICurrencyService
	{
		Task<CurrencyConversion> GetCurrenciesFor(CurrencySymbol forCurrency,decimal amount);
		Task<IEnumerable<CurrencySymbol>> GetCurrencySombols();
	}
}
