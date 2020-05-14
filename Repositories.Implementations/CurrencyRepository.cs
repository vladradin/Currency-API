using DtoModels;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
	public class CurrencyRepository : RepositoryBase<CurrencyDTO>, ICurrencyRepository
	{
		public CurrencyRepository(ConnectionSettings connectionSettings, IDatabaseFactory databaseFactory) 
			: base(connectionSettings, databaseFactory,"currencies")
		{
		}
	}
}
