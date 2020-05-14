using DtoModels;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyPortal.Tests.Mockups
{
	class MockCurrencyRepository : ICurrencyRepository
	{
		public IList<CurrencyDTO> Currencies { get; set; } = new List<CurrencyDTO>();

		public Task<CurrencyDTO> Add(CurrencyDTO newItem)
		{
			Currencies.Add(newItem);
			return Task.FromResult(newItem);
		}

		public Task Add(IEnumerable<CurrencyDTO> items)
		{
			foreach (var item in items)
				Currencies.Add(item);

			return Task.CompletedTask;
		}

		public Task<IEnumerable<CurrencyDTO>> GetAll()
			=>Task.FromResult<IEnumerable<CurrencyDTO>>(Currencies);

		public Task<CurrencyDTO> GetById(string itemId)
			=> Task.FromResult(Currencies.FirstOrDefault(curr => curr.Id == itemId));
		

		public Task<bool> Remove(string itemId)
		{
			throw new NotImplementedException();
		}

		public Task<bool> Update(string itemId, CurrencyDTO updateItem)
		{
			throw new NotImplementedException();
		}
	}
}
