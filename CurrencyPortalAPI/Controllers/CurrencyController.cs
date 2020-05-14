using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bussiness.Models;
using CurrencyPortalAPI.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace CurrencyPortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	[Authorize]
    public class CurrencyController : ControllerBase
    {
		private readonly ICurrencyService _currencyService;

		public CurrencyController(ICurrencyService currencyService)
			=> _currencyService = currencyService;

		[HttpPost("converter")]
		public async Task<CurrencyConversionViewModel> GetCurrenciesFrom([FromBody]CurrencyViewModel fromCurrency)
		{
			var currencySymbol = new CurrencySymbol(fromCurrency.Symbol);
			var amount = fromCurrency.Amount;

			var currencyConversion = await _currencyService.GetCurrenciesFor(currencySymbol,amount);

			return new CurrencyConversionViewModel(currencyConversion,fromCurrency.Amount);
			
		}

		[HttpGet("list")]
		public async Task<IEnumerable<string>> GetCurrenciesSymbols()
		{
			var currencies = await _currencyService.GetCurrencySombols();
			return currencies.Select(currencySymbol => currencySymbol.ToString());
		}
	}
}