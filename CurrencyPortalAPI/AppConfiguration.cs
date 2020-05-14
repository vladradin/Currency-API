using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bussiness.Models;
using Repositories.Interfaces;
using UserManagement;

namespace CurrencyPortalAPI
{
	public class AppConfiguration
	{
		public JwtConfig JwtSettings { get; set; }
		public ConnectionSettings ConnectionSettings { get; set; }
		public CurrencyClientConfig ClientConfig { get; set; }
	}
}
