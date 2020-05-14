using Bussiness.Models;
using Newtonsoft.Json;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
	public class CurrencyApiResponse
	{
		[JsonConverter(typeof(CurrencyJsonConverter))]
		public IEnumerable<Currency> Rates { get; set; }

		[JsonProperty("base")]
		[JsonConverter(typeof(SymbolJsonConverter))]
		public CurrencySymbol FromCurrency { get; set; }
		public DateTime Date { get; set; }

	}

	public class CurrencyClient : ICurrencyClient
	{
		private readonly HttpClient client = new HttpClient();

		private readonly CurrencyClientConfig _clientConfig;

		public CurrencyClient(CurrencyClientConfig clientConfig)
			=> _clientConfig = clientConfig;

		private string GetUrl() 
			=> $"{_clientConfig.Protocol}://{_clientConfig.Endpoint}";

		public async Task<CurrencyConversion> GetCurrenciesFor(CurrencySymbol fromCurrency)
		{
			var fromCurrencyQueryParameter = $"base={fromCurrency.ToString()}";

			var currencyConversion = await GetCurrenciesByUrl(fromCurrencyQueryParameter);

			return currencyConversion;
		}

		public async Task<IEnumerable<CurrencySymbol>> GetCurrencies()
		{
			var currencyConversion = await GetCurrenciesByUrl("");

			var allSymbolsConvertedInto = currencyConversion.ToCurrencies.Select(currency => currency.Symbol);

			var allOfTheSymbols = allSymbolsConvertedInto.Append(currencyConversion.From);

			return allOfTheSymbols;
		}

		private async Task<CurrencyConversion> GetCurrenciesByUrl(string parameters)
		{
			var queryParamsters = parameters.Length > 0
							? $"?{parameters}"
							: "";

			var urlPath = $"{GetUrl()}/latest{queryParamsters}";

			var jsonResponse = await client.GetStringAsync(urlPath);

			var currencyApiResponse = JsonConvert.DeserializeObject<CurrencyApiResponse>(jsonResponse);

			return new CurrencyConversion(currencyApiResponse.FromCurrency, currencyApiResponse.Rates, currencyApiResponse.Date);
		}
	}
}
