using Bussiness.Models;
using DtoModels;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
	public class CurrencyService : ICurrencyService
	{
		private readonly ICurrencyClient _currencyClient;
		private readonly ICurrencyRepository _currencyRepo;

		public CurrencyService(ICurrencyClient currencyClient, ICurrencyRepository currencyRepository)
		{
			_currencyClient = currencyClient;
			_currencyRepo = currencyRepository;
		}

		public async Task<CurrencyConversion> GetCurrenciesFor(CurrencySymbol forCurrency, decimal amount)
		{			
			var singleUnitConversion =  await _currencyClient.GetCurrenciesFor(forCurrency);

			return CalculateConversionForAmount(singleUnitConversion, amount);
		}

		private CurrencyConversion CalculateConversionForAmount(CurrencyConversion singleMoneyUnitConversion, decimal amount)
		{
			return new CurrencyConversion(
				singleMoneyUnitConversion.From,
				singleMoneyUnitConversion.ToCurrencies.Select(CurrencyRateFor(amount)),
				singleMoneyUnitConversion.OnDate);
		}

		private Func<Currency, Currency> CurrencyRateFor(decimal amount)
			=> singleUnitCurrency => new Currency
			{
				Symbol = singleUnitCurrency.Symbol,
				Amount = Math.Round(singleUnitCurrency.Amount * amount,3)
			};
		

		public async Task<IEnumerable<CurrencySymbol>> GetCurrencySombols()
		{
			var currenciesStoredLocally = await _currencyRepo.GetAll();
			if (currenciesStoredLocally.Any())
				return currenciesStoredLocally.Select(ToSymbols);
			else
			{
				var curencySymbols = await _currencyClient.GetCurrencies();
				var currencyDtos = curencySymbols.Select(symbol => ToCurrencyDto(symbol));
				await _currencyRepo.Add(currencyDtos);
				return curencySymbols;
			}
		}

		private CurrencyDTO ToCurrencyDto(CurrencySymbol symbol)
			=> new CurrencyDTO { Id = symbol.ToString() };

		private CurrencySymbol ToSymbols(CurrencyDTO currencyDto)
			=> new CurrencySymbol(currencyDto.Id);
	}
}
