using Bussiness.Models;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyPortal.Tests.Mockups
{
	public class MockCurrencyClient : ICurrencyClient
	{
		public Task<IEnumerable<CurrencySymbol>> GetCurrencies()
		{
			throw new NotImplementedException();
		}

		public Task<CurrencyConversion> GetCurrenciesFor(CurrencySymbol fromCurrency)
		{
			throw new NotImplementedException();
		}
	}
}
